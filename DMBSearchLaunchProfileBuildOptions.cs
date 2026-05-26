using System;

namespace DMBSearchBuilder
{
    /// <summary>
    /// Describes a search build that may start an ASP.NET Core project from a launch profile before crawling it.
    /// </summary>
    public sealed class DMBSearchLaunchProfileBuildOptions
    {
        /// <summary>
        /// Gets or sets the ASP.NET Core project file used when the website must be started.
        /// </summary>
        public string ProjectPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the launch settings file that contains the website profiles.
        /// </summary>
        public string LaunchSettingsPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the preferred launch profile name.
        /// </summary>
        public string LaunchProfileName { get; set; } = "https";

        /// <summary>
        /// Gets or sets the fallback launch profile name used when <see cref="LaunchProfileName"/> is missing.
        /// </summary>
        public string? FallbackLaunchProfileName { get; set; }

        /// <summary>
        /// Gets or sets the SQLite database path that receives generated search records.
        /// </summary>
        public string DatabasePath { get; set; } = "Search/data.db";

        /// <summary>
        /// Gets or sets the maximum number of pages to crawl during a build.
        /// </summary>
        public int MaxPages { get; set; } = 500;

        /// <summary>
        /// Gets or sets the maximum request duration for one crawled page.
        /// </summary>
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Gets or sets the maximum duration allowed for a temporary website startup.
        /// </summary>
        public TimeSpan StartupTimeout { get; set; } = TimeSpan.FromSeconds(90);

        /// <summary>
        /// Gets the path prefixes ignored by the crawler.
        /// </summary>
        public List<string> ExcludedPathPrefixes { get; } = ["/Documentation", "/Search"];

        /// <summary>
        /// Gets or sets a value indicating whether query strings are kept during crawl URL normalization.
        /// </summary>
        public bool PreserveQueryStrings { get; set; }

        /// <summary>
        /// Gets or sets the user agent sent by the crawler.
        /// </summary>
        public string UserAgent { get; set; } = "DMBSearchBuilder/0.9";
    }
}
