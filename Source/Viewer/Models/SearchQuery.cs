#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Represents a search request sent to <see cref="ISearchProvider" /> implementations.
    /// </summary>
    public sealed class SearchQuery
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the maximum number of results requested from one provider.
        /// </summary>
        public int MaxResults { get; set; } = 20;

        /// <summary>
        ///     Gets or sets the user-entered search term.
        /// </summary>
        public string Term { get; set; } = string.Empty;

        #endregion
    }
}