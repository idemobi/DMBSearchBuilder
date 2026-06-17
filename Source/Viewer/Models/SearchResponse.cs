#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Contains merged search results and provider errors for one user query.
    /// </summary>
    public sealed class SearchResponse
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the provider errors captured during execution.
        /// </summary>
        public List<SearchProviderError> Errors { get; } = [];

        /// <summary>
        ///     Gets the merged search results.
        /// </summary>
        public List<SearchResult> Results { get; } = [];

        #endregion
    }
}