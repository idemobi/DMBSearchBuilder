#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBServerHelper;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Executes all registered <see cref="ISearchProvider" /> implementations and merges their results.
    /// </summary>
    public sealed class SearchCompositeAgent
    {
        #region Instance fields and properties

        private readonly IEnumerable<ISearchProvider> _providers;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SearchCompositeAgent" /> class.
        /// </summary>
        /// <param name="providers">The registered search providers.</param>
        public SearchCompositeAgent(IEnumerable<ISearchProvider> providers)
        {
            _providers = providers;
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Executes the search across all registered providers.
        /// </summary>
        /// <param name="query">The search query to execute.</param>
        /// <param name="cancellationToken">A token used to cancel the query.</param>
        /// <returns>A response containing merged results and provider errors.</returns>
        public async Task<SearchResponse> SearchAsync(SearchQuery query, CancellationToken cancellationToken)
        {
            SearchResponse response = new();

            foreach (ISearchProvider provider in _providers)
            {
                try
                {
                    IReadOnlyList<SearchResult> results = await provider.SearchAsync(query, cancellationToken).ConfigureAwait(false);
                    response.Results.AddRange(results);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception)
                {
                    response.Errors.Add(new SearchProviderError
                    {
                        ProviderName = provider.Name,
                        Message = WebLocalizer.GetInternal("ERROR_SEARCH_PROVIDER_UNAVAILABLE_MESSAGE").Value
                    });
                }
            }

            response.Results.Sort((left, right) => right.Score.CompareTo(left.Score));
            return response;
        }

        #endregion
    }
}