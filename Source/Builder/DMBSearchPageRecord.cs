#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System;
using System.Collections.Generic;

namespace DMBSearchBuilder
{
    /// <summary>
    ///     Represents one crawled page stored in the DMB search index.
    /// </summary>
    public sealed class DMBSearchPageRecord
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the page content length in characters.
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        ///     Gets or sets the meta description or text excerpt for the page.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the normalized keywords extracted from the page title, description, headings, and body text.
        /// </summary>
        public string Keywords { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the page title extracted from the HTML document.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the canonical absolute URL of the crawled page.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        ///     Gets weighted normalized terms extracted from relevant page areas.
        /// </summary>
        public Dictionary<string, int> WeightedTerms { get; } = new(StringComparer.Ordinal);

        #endregion
    }
}