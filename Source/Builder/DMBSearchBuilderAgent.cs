#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace DMBSearchBuilder
{
    /// <summary>
    ///     Crawls a web site and writes a SQLite search index consumed by DMBSearchViewer.
    /// </summary>
    public sealed class DMBSearchBuilderAgent
    {
        #region Static fields and properties

        private static readonly Regex LinkRegex = new("<a\\s+[^>]*href=[\"']([^\"'#]+)[\"'][^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion

        #region Static methods

        private static string BuildStoredUrl(Uri uri)
        {
            string path = string.IsNullOrWhiteSpace(uri.AbsolutePath) ? "/" : uri.AbsolutePath;

            if (!string.IsNullOrWhiteSpace(uri.Query))
            {
                path += uri.Query;
            }

            return path;
        }

        private static IEnumerable<Uri> ExtractSameSiteLinks(
            Uri baseUri,
            Uri pageUri,
            string html,
            IReadOnlyCollection<string> excludedPathPrefixes,
            bool preserveQueryStrings
        )
        {
            foreach (Match match in LinkRegex.Matches(html))
            {
                string rawHref = WebUtility.HtmlDecode(match.Groups[1].Value) ?? string.Empty;

                if (string.IsNullOrWhiteSpace(rawHref))
                {
                    continue;
                }

                if (!Uri.TryCreate(pageUri, rawHref, out Uri? candidate))
                {
                    continue;
                }

                Uri normalized = NormalizeUri(candidate, preserveQueryStrings);

                if (!string.Equals(normalized.Host, baseUri.Host, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!string.Equals(normalized.Scheme, baseUri.Scheme, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (IsExcluded(normalized, excludedPathPrefixes))
                {
                    continue;
                }

                yield return normalized;
            }
        }

        private static bool IsExcluded(Uri uri, IReadOnlyCollection<string> excludedPathPrefixes)
        {
            foreach (string excludedPathPrefix in excludedPathPrefixes)
            {
                if (string.IsNullOrWhiteSpace(excludedPathPrefix))
                {
                    continue;
                }

                if (uri.AbsolutePath.StartsWith(excludedPathPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsHtmlResponse(HttpResponseMessage response)
        {
            string? mediaType = response.Content.Headers.ContentType?.MediaType;

            return string.IsNullOrWhiteSpace(mediaType) ||
                   string.Equals(mediaType, "text/html", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(mediaType, "application/xhtml+xml", StringComparison.OrdinalIgnoreCase);
        }

        private static Uri NormalizeUri(Uri uri, bool preserveQueryStrings)
        {
            UriBuilder builder = new(uri)
            {
                Fragment = string.Empty,
                Query = preserveQueryStrings ? uri.Query.TrimStart('?') : string.Empty
            };

            return builder.Uri;
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Crawls the configured site and rebuilds the SQLite search index.
        /// </summary>
        /// <param name="options">The build options used for crawling and storage.</param>
        /// <param name="cancellationToken">A token used to cancel the crawl.</param>
        /// <returns>The number of pages written to the search database.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="options" /> is <see langword="null" />.</exception>
        public async Task<int> BuildAsync(DMBSearchBuildOptions options, CancellationToken cancellationToken = default)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            Console.WriteLine($"[DMBSearchBuilder] Starting crawl from {options.BaseUri}");
            Console.WriteLine($"[DMBSearchBuilder] Database path: {options.DatabasePath}");
            Console.WriteLine($"[DMBSearchBuilder] Max pages: {options.MaxPages}");
            Console.WriteLine($"[DMBSearchBuilder] Excluded paths: {string.Join(", ", options.ExcludedPathPrefixes)}");

            using HttpClient client = new();
            client.Timeout = options.RequestTimeout;
            client.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);

            Queue<Uri> queue = new();
            HashSet<string> visited = new(StringComparer.OrdinalIgnoreCase);
            List<DMBSearchPageRecord> records = [];
            Uri normalizedBaseUri = NormalizeUri(options.BaseUri, options.PreserveQueryStrings);

            if (!IsExcluded(normalizedBaseUri, options.ExcludedPathPrefixes))
            {
                queue.Enqueue(normalizedBaseUri);
            }
            else
            {
                Console.WriteLine($"[DMBSearchBuilder] Start page excluded: {normalizedBaseUri}");
            }

            int savedCount = 0;

            while (queue.Count > 0 && savedCount < options.MaxPages)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Uri pageUri = queue.Dequeue();
                string pageKey = pageUri.ToString();

                if (!visited.Add(pageKey))
                {
                    continue;
                }

                string html;

                try
                {
                    Console.WriteLine($"[DMBSearchBuilder] Crawling: {pageUri}");
                    using HttpRequestMessage request = new(HttpMethod.Get, pageUri);
                    using HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[DMBSearchBuilder] Skipped HTTP {(int)response.StatusCode}: {pageUri}");
                        continue;
                    }

                    if (!IsHtmlResponse(response))
                    {
                        string mediaType = response.Content.Headers.ContentType?.MediaType ?? "unknown";
                        Console.WriteLine($"[DMBSearchBuilder] Skipped non-HTML ({mediaType}): {pageUri}");
                        continue;
                    }

                    html = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine($"[DMBSearchBuilder] Skipped request error: {pageUri}");
                    continue;
                }
                catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine($"[DMBSearchBuilder] Skipped timeout: {pageUri}");
                    continue;
                }

                DMBSearchPageRecord record = DMBSearchKeywordExtractor.ExtractPage(BuildStoredUrl(pageUri), html);
                records.Add(record);
                savedCount++;
                Console.WriteLine($"[DMBSearchBuilder] Indexed: {record.Title} ({pageUri})");

                foreach (Uri link in ExtractSameSiteLinks(options.BaseUri, pageUri, html, options.ExcludedPathPrefixes, options.PreserveQueryStrings))
                {
                    string linkKey = link.ToString();

                    if (!visited.Contains(linkKey) && visited.Count + queue.Count < options.MaxPages * 3)
                    {
                        queue.Enqueue(link);
                    }
                }
            }

            if (records.Count > 0)
            {
                DMBSearchDatabaseManager.ClearPages(options.DatabasePath);

                foreach (DMBSearchPageRecord record in records)
                {
                    DMBSearchDatabaseManager.SavePage(options.DatabasePath, record);
                }
            }
            else
            {
                Console.WriteLine("[DMBSearchBuilder] No pages indexed. Existing database was not changed.");
            }

            Console.WriteLine($"[DMBSearchBuilder] Finished. Indexed pages: {records.Count}. Visited URLs: {visited.Count}.");

            return savedCount;
        }

        /// <summary>
        ///     Ensures a launch-profile website is available, crawls it, and rebuilds the SQLite search index.
        /// </summary>
        /// <param name="options">The launch profile and crawler options.</param>
        /// <param name="cancellationToken">A token used to cancel the crawl.</param>
        /// <returns>The number of pages written to the search database.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="options" /> is <see langword="null" />.</exception>
        public async Task<int> BuildFromLaunchProfileAsync(DMBSearchLaunchProfileBuildOptions options, CancellationToken cancellationToken = default)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            using DMBSearchWebsiteHost websiteHost = await DMBSearchWebsiteHost.EnsureAvailableAsync(
                options.ProjectPath,
                options.LaunchSettingsPath,
                options.LaunchProfileName,
                options.FallbackLaunchProfileName,
                options.StartupTimeout,
                options.UseNoBuild).ConfigureAwait(false);

            DMBSearchBuildOptions buildOptions = new()
            {
                BaseUri = websiteHost.BaseUri,
                DatabasePath = options.DatabasePath,
                MaxPages = options.MaxPages,
                RequestTimeout = options.RequestTimeout,
                PreserveQueryStrings = options.PreserveQueryStrings,
                UserAgent = options.UserAgent
            };

            buildOptions.ExcludedPathPrefixes.Clear();
            buildOptions.ExcludedPathPrefixes.AddRange(options.ExcludedPathPrefixes);

            return await BuildAsync(buildOptions, cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}