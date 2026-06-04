#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBSearchViewer.Resources;
using DMBServerWebHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Configures DMBSearchViewer services for a host web application.
    /// </summary>
    [Serializable]
    public class DMBSearchViewerConfiguration : WebGenericConfiguration<DMBSearchViewerConfiguration>, IServerWebConfig
    {
        #region Static methods

        /// <summary>
        ///     Maps DMBSearchViewer endpoints on the host application.
        /// </summary>
        /// <param name="app">The configured web application.</param>
        /// <remarks>
        ///     DMBSearchViewer uses MVC controller discovery and does not need a dedicated endpoint mapping.
        /// </remarks>
        public static void UseApp(WebApplication app)
        {
        }

        #endregion

        #region Instance methods

        #region From interface IServerWebConfig

        /// <summary>
        ///     Registers search providers, the navbar menu provider, and search options.
        /// </summary>
        /// <param name="appBuilder">The host application builder used to register services.</param>
        /// <param name="configBuilder">The configuration builder supplied by the host pipeline.</param>
        /// <param name="configRoot">The resolved configuration root supplied by the host pipeline.</param>
        public override void AfterConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
            appBuilder.Services.Configure<DMBSearchViewerOptions>(configRoot.GetSection("DMBSearchViewer"));
            appBuilder.Services.AddTransient<IProfileBarSectionProvider, DMBSearchNavbarProfileBarSectionProvider>();
            appBuilder.Services.AddTransient<IDMBSearchProvider, DMBSearchBuilderSearchProvider>();
            appBuilder.Services.AddTransient<IDMBSearchProvider, DMBDocumentationViewerSearchProvider>();
            appBuilder.Services.AddTransient<DMBSearchCompositeAgent>();
            AddAnnotationLocalization(
                appBuilder,
                typeof(DMBSearchViewerDataAnnotationLocalization),
                typeof(DMBSearchViewerInternalLocalization));
        }

        /// <summary>
        ///     Indicates whether this package contributes an API description surface.
        /// </summary>
        /// <returns><see langword="false" /> because the viewer renders MVC pages.</returns>
        public override bool ApiDescription()
        {
            return false;
        }

        /// <summary>
        ///     Runs before host configuration is completed.
        /// </summary>
        /// <param name="appBuilder">The host application builder supplied by the configuration pipeline.</param>
        /// <param name="configBuilder">The configuration builder supplied by the configuration pipeline.</param>
        /// <param name="configRoot">The resolved configuration root supplied by the configuration pipeline.</param>
        public override void BeforeConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
        }

        /// <summary>
        ///     Indicates whether the package requires a configuration file or application settings section.
        /// </summary>
        /// <returns><see langword="false" /> because default database paths are provided.</returns>
        public override bool NeedsConfigFileOrAppSettings()
        {
            return false;
        }

        /// <summary>
        ///     Populates randomized configuration values for development scenarios.
        /// </summary>
        public override void RandomFake()
        {
        }

        #endregion

        #endregion
    }
}