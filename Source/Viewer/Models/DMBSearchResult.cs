#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Represents one normalized search result shown by DMBSearchViewer.
    /// </summary>
    public sealed class DMBSearchResult
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the absolute URL displayed to users.
        /// </summary>
        public string DisplayUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the short text excerpt displayed below the result title.
        /// </summary>
        public string Excerpt { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the score used to order result groups.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        ///     Gets or sets the provider display name that returned the result.
        /// </summary>
        public string SourceName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the result title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the result URL.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        #endregion
    }
}