#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Renders the DMBSearchViewer search result and demo pages.
    /// </summary>
    public sealed class SearchController : RawBootstrapController
    {
        #region Instance fields and properties

        private readonly DMBSearchViewerOptions _options;
        private readonly DMBSearchCompositeAgent _searchAgent;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="searchAgent">The search agent used to query registered providers.</param>
        /// <param name="options">The configured search viewer options.</param>
        public SearchController(DMBSearchCompositeAgent searchAgent, IOptions<DMBSearchViewerOptions> options)
        {
            _searchAgent = searchAgent;
            _options = options.Value;
        }

        #endregion

        #region Instance methods

        private void ApplyDisplayUrls(IEnumerable<DMBSearchResult> results)
        {
            foreach (DMBSearchResult result in results)
            {
                result.DisplayUrl = BuildDisplayUrl(result.Url);
            }
        }

        private string BuildDisplayUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? absoluteUri))
            {
                if (!absoluteUri.IsLoopback)
                {
                    return absoluteUri.ToString();
                }

                url = absoluteUri.PathAndQuery;
            }

            string normalizedPath = url.TrimStart('/');
            string displayUrl = ServerHelperConfiguration.Config.ComposeUrlWithPath(normalizedPath);
            if (Uri.TryCreate(displayUrl, UriKind.Absolute, out _))
            {
                return displayUrl;
            }

            return url;
        }

        /// <summary>
        ///     Shows the manual preview page for the DMBSearchViewer visual states.
        /// </summary>
        /// <returns>The DMBSearchViewer demo page.</returns>
        [HttpGet]
        public IActionResult Demo()
        {
            SetTitle("Search Demo");
            SetDescription("Manual preview states for the DMBSearchViewer search component.");
            SetKeywords("search", "demo", "preview");

            DMBSearchPageViewModel model = new()
            {
                Term = "navbar",
                IsDemo = true
            };

            model.Results.AddRange(
            [
                new DMBSearchResult
                {
                    SourceName = "DMBSearchBuilder",
                    Title = "BootstrapBuilder Navigation Bar",
                    Url = "/BootstrapBuilder/Architecture",
                    Excerpt = "Explains how navbar providers contribute menu and profile sections.",
                    Score = 100
                },
                new DMBSearchResult
                {
                    SourceName = "DMBDocumentationViewer",
                    Title = "BasicNavigationBarComposer (class)",
                    Url = "/Documentation/RenderFromDatabase?objectName=BasicNavigationBarComposer",
                    Excerpt = "DMBBootstrapBuilder 0.9.0 DMBBootstrapBuilder",
                    Score = 95
                }
            ]);

            model.Errors.Add(new DMBSearchProviderError
            {
                ProviderName = "DemoErrorProvider",
                Message = "The provider could not return results."
            });

            ApplyDisplayUrls(model.Results);

            return View("~/Views/Search/Demo.cshtml", model);
        }

        /// <summary>
        ///     Shows the search result listing page.
        /// </summary>
        /// <param name="term">The user-entered search term.</param>
        /// <param name="cancellationToken">A token used to cancel provider queries.</param>
        /// <returns>The search result page view.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string? term, CancellationToken cancellationToken)
        {
            string normalizedTerm = (term ?? string.Empty).Trim();

            string title = WebLocalizer.GetInternal("VIEW_SEARCH_INDEX_TITLE").Value;
            SetTitle(string.IsNullOrWhiteSpace(normalizedTerm) ? title : $"{title} - {normalizedTerm}");
            SetDescription(WebLocalizer.GetInternal("VIEW_SEARCH_INDEX_DESCRIPTION").Value);
            SetKeywords(WebLocalizer.GetInternal("VIEW_SEARCH_INDEX_KEYWORDS").Value);

            DMBSearchPageViewModel model = new()
            {
                Term = normalizedTerm
            };

            if (!string.IsNullOrWhiteSpace(normalizedTerm))
            {
                DMBSearchResponse response = await _searchAgent.SearchAsync(new DMBSearchQuery
                {
                    Term = normalizedTerm,
                    MaxResults = Math.Max(1, _options.MaxResultsPerProvider)
                }, cancellationToken).ConfigureAwait(false);

                model.Results.AddRange(response.Results);
                model.Errors.AddRange(response.Errors);
            }

            ApplyDisplayUrls(model.Results);

            return View("~/Views/Search/Index.cshtml", model);
        }

        #endregion
    }
}