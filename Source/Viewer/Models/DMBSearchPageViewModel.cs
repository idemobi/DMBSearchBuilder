#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System.Collections.Generic;

namespace DMBSearchViewer
{
    /// <summary>
    ///     Represents the search result page model rendered by DMBSearchViewer.
    /// </summary>
    public sealed class DMBSearchPageViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the provider errors captured during execution.
        /// </summary>
        public List<DMBSearchProviderError> Errors { get; } = [];

        /// <summary>
        ///     Gets or sets a value indicating whether the page represents a demo state.
        /// </summary>
        public bool IsDemo { get; set; }

        /// <summary>
        ///     Gets the merged search results.
        /// </summary>
        public List<DMBSearchResult> Results { get; } = [];

        /// <summary>
        ///     Gets or sets the user-entered search term.
        /// </summary>
        public string Term { get; set; } = string.Empty;

        #endregion
    }
}