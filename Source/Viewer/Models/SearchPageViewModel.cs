#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Represents the search result page model rendered by DMBSearchViewer.
    /// </summary>
    public sealed class SearchPageViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the provider errors captured during execution.
        /// </summary>
        public List<SearchProviderError> Errors { get; } = [];

        /// <summary>
        ///     Gets or sets a value indicating whether the page represents a demo state.
        /// </summary>
        public bool IsDemo { get; set; }

        /// <summary>
        ///     Gets the merged search results.
        /// </summary>
        public List<SearchResult> Results { get; } = [];

        /// <summary>
        ///     Gets or sets the user-entered search term.
        /// </summary>
        public string Term { get; set; } = string.Empty;

        #endregion
    }
}