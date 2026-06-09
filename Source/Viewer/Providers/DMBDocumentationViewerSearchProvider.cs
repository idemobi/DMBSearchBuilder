#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBSearchCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Searches the SQLite database generated for DMBDocumentationViewer pages.
    /// </summary>
    public sealed class DMBDocumentationViewerSearchProvider : IDMBSearchProvider
    {
        #region Static methods

        private static double ComputeScore(
            IReadOnlyList<string> tokens,
            string normalizedTerm,
            string objectName,
            string namespaceName,
            string objectType,
            string packageId,
            string version,
            string technicalKeywords,
            string keywords
        )
        {
            string normalizedObjectName = DMBSearchTextNormalizer.NormalizeSearchText(objectName);
            string normalizedNamespaceName = DMBSearchTextNormalizer.NormalizeSearchText(namespaceName);
            string normalizedObjectType = DMBSearchTextNormalizer.NormalizeSearchText(objectType);
            string normalizedPackage = DMBSearchTextNormalizer.NormalizeSearchText($"{packageId} {version}");
            string normalizedTechnicalKeywords = DMBSearchTextNormalizer.NormalizeSearchText(technicalKeywords);
            string normalizedKeywords = DMBSearchTextNormalizer.NormalizeSearchText(keywords);

            double tokenScore = 0;

            foreach (string token in tokens)
            {
                double currentTokenScore = 0;

                currentTokenScore += ContainsToken(normalizedObjectName, token) ? 45 : 0;
                currentTokenScore += ContainsToken(normalizedTechnicalKeywords, token) ? 28 : 0;
                currentTokenScore += ContainsToken(normalizedNamespaceName, token) ? 15 : 0;
                currentTokenScore += ContainsToken(normalizedObjectType, token) ? 10 : 0;
                currentTokenScore += ContainsToken(normalizedKeywords, token) ? 10 : 0;
                currentTokenScore += ContainsToken(normalizedPackage, token) ? 6 : 0;

                if (currentTokenScore <= 0)
                {
                    return 0;
                }

                tokenScore += currentTokenScore;
            }

            double score = tokenScore / tokens.Count;

            if (ContainsPhrase(normalizedObjectName, normalizedTerm))
            {
                score += 25;
            }
            else if (ContainsPhrase(normalizedTechnicalKeywords, normalizedTerm))
            {
                score += 15;
            }
            else if (ContainsPhrase(normalizedKeywords, normalizedTerm))
            {
                score += 8;
            }

            return Math.Min(100, score);
        }

        private static bool ContainsPhrase(string normalizedValue, string normalizedTerm)
        {
            if (normalizedTerm.Contains(' ', StringComparison.Ordinal) &&
                normalizedValue.Contains(normalizedTerm, StringComparison.Ordinal))
            {
                return true;
            }

            string compactValue = DMBSearchTextNormalizer.CompactSearchText(normalizedValue);
            string compactTerm = DMBSearchTextNormalizer.CompactSearchText(normalizedTerm);

            return compactTerm.Length >= 4 && compactValue.Contains(compactTerm, StringComparison.Ordinal);
        }

        private static bool ContainsToken(string normalizedValue, string token)
        {
            if (DMBSearchTextNormalizer.CompactSearchText(normalizedValue).Contains(token, StringComparison.Ordinal))
            {
                return true;
            }

            return normalizedValue
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Contains(token, StringComparer.Ordinal);
        }

        #endregion

        #region Instance fields and properties

        private readonly IWebHostEnvironment _environment;
        private readonly DMBSearchViewerOptions _options;

        #region From interface IDMBSearchProvider

        /// <summary>
        ///     Gets the provider display name.
        /// </summary>
        public string Name => "DMBDocumentationViewer";

        #endregion

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DMBDocumentationViewerSearchProvider" /> class.
        /// </summary>
        /// <param name="environment">The host environment used to resolve relative database paths.</param>
        /// <param name="options">The configured DMBSearchViewer options.</param>
        public DMBDocumentationViewerSearchProvider(IWebHostEnvironment environment, IOptions<DMBSearchViewerOptions> options)
        {
            _environment = environment;
            _options = options.Value;
        }

        #endregion

        #region Instance methods

        #region From interface IDMBSearchProvider

        /// <summary>
        ///     Searches the generated documentation database.
        /// </summary>
        /// <param name="query">The search query to execute.</param>
        /// <param name="cancellationToken">A token used to cancel the provider query.</param>
        /// <returns>A list of normalized search results.</returns>
        public Task<IReadOnlyList<DMBSearchResult>> SearchAsync(DMBSearchQuery query, CancellationToken cancellationToken)
        {
            string databasePath = DMBSearchPathResolver.Resolve(_environment, _options.DocumentationDatabasePath);

            if (!File.Exists(databasePath) || string.IsNullOrWhiteSpace(query.Term))
            {
                return Task.FromResult<IReadOnlyList<DMBSearchResult>>([]);
            }

            string normalizedTerm = DMBSearchTextNormalizer.NormalizeSearchText(query.Term);
            IReadOnlyList<string> tokens = DMBSearchTextNormalizer.ExtractSearchTokens(query.Term);

            if (string.IsNullOrWhiteSpace(normalizedTerm) || tokens.Count == 0)
            {
                return Task.FromResult<IReadOnlyList<DMBSearchResult>>([]);
            }

            using SqliteConnection connection = new($"Data Source={databasePath}");
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = """
                                  SELECT PackageId, Version, NamespaceName, ObjectName, ObjectType, RoutePath, TechnicalKeywords, Keywords
                                  FROM DocumentationObjects
                                  WHERE NamespaceName <> '<global namespace>'
                                    AND ObjectName <> '<global namespace>'
                                  ORDER BY ObjectName;
                                  """;

            using SqliteDataReader reader = command.ExecuteReader();
            List<DMBSearchResult> results = [];

            while (reader.Read())
            {
                cancellationToken.ThrowIfCancellationRequested();

                string objectName = reader["ObjectName"]?.ToString() ?? string.Empty;
                string objectType = reader["ObjectType"]?.ToString() ?? string.Empty;
                string namespaceName = reader["NamespaceName"]?.ToString() ?? string.Empty;
                string packageId = reader["PackageId"]?.ToString() ?? string.Empty;
                string version = reader["Version"]?.ToString() ?? string.Empty;
                string technicalKeywords = reader["TechnicalKeywords"]?.ToString() ?? string.Empty;
                string keywords = reader["Keywords"]?.ToString() ?? string.Empty;
                double score = ComputeScore(tokens, normalizedTerm, objectName, namespaceName, objectType, packageId, version, technicalKeywords, keywords);

                if (score <= 0)
                {
                    continue;
                }

                results.Add(new DMBSearchResult
                {
                    SourceName = Name,
                    Title = string.IsNullOrWhiteSpace(objectType) ? objectName : $"{objectName} ({objectType})",
                    Url = reader["RoutePath"]?.ToString() ?? string.Empty,
                    Excerpt = $"{packageId} {version} {namespaceName}".Trim(),
                    Score = score
                });
            }

            return Task.FromResult<IReadOnlyList<DMBSearchResult>>(results
                .OrderByDescending(static result => result.Score)
                .ThenBy(static result => result.Title, StringComparer.OrdinalIgnoreCase)
                .Take(query.MaxResults)
                .ToArray());
        }

        #endregion

        #endregion
    }
}