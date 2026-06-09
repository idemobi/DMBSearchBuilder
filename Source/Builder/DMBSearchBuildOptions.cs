#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchBuilder
{
    /// <summary>
    ///     Describes one search index build operation for <see cref="DMBSearchBuilderAgent" />.
    /// </summary>
    public sealed class DMBSearchBuildOptions
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the absolute site URI used as the crawler entry point.
        /// </summary>
        public Uri BaseUri { get; set; } = new("http://localhost:5000/");

        /// <summary>
        ///     Gets or sets the SQLite database path that receives generated search records.
        /// </summary>
        public string DatabasePath { get; set; } = "Search/data.db";

        /// <summary>
        ///     Gets the path prefixes ignored by the crawler.
        /// </summary>
        public List<string> ExcludedPathPrefixes { get; } = ["/Documentation", "/Search"];

        /// <summary>
        ///     Gets or sets the maximum number of pages to crawl during a build.
        /// </summary>
        public int MaxPages { get; set; } = 500;

        /// <summary>
        ///     Gets or sets a value indicating whether query strings are kept during crawl URL normalization.
        /// </summary>
        public bool PreserveQueryStrings { get; set; }

        /// <summary>
        ///     Gets or sets the maximum request duration for one crawled page.
        /// </summary>
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        ///     Gets or sets the user agent sent by the crawler.
        /// </summary>
        public string UserAgent { get; set; } = "DMBSearchBuilder/0.9";

        #endregion
    }
}