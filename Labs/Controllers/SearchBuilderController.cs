#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DMBSearchBuilderLabs.Controllers
{
    /// <summary>
    ///     Provides documentation pages for the DMBSearchBuilder package.
    /// </summary>
    public class SearchBuilderController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the SearchBuilder architecture page.
        /// </summary>
        /// <returns>The architecture view.</returns>
        public IActionResult Architecture()
        {
            SetTitle("SearchBuilder - Architecture");
            SetDescription("SearchBuilder architecture");
            SetKeywords("SearchBuilder", "DMBSearchBuilder", "Architecture", "Crawler", "SQLite");
            return View();
        }

        /// <summary>
        ///     Renders the SearchBuilder getting started page.
        /// </summary>
        /// <returns>The getting started view.</returns>
        public IActionResult GettingStarted()
        {
            SetTitle("SearchBuilder - Getting Started");
            SetDescription("SearchBuilder getting started guide");
            SetKeywords("SearchBuilder", "DMBSearchBuilder", "Getting Started", "PreBuilding");
            return View();
        }

        /// <summary>
        ///     Renders the SearchBuilder introduction page.
        /// </summary>
        /// <returns>The introduction view.</returns>
        public IActionResult Introduction()
        {
            SetTitle("SearchBuilder - Introduction");
            SetDescription("SearchBuilder");
            SetKeywords("SearchBuilder", "DMBSearchBuilder", "Search", "Crawler", "SQLite");
            return View();
        }

        /// <summary>
        ///     Renders the SearchBuilder rendering pipeline page.
        /// </summary>
        /// <returns>The rendering pipeline view.</returns>
        public IActionResult RenderingPipeline()
        {
            SetTitle("SearchBuilder - Rendering Pipeline");
            SetDescription("SearchBuilder rendering pipeline");
            SetKeywords("SearchBuilder", "DMBSearchBuilder", "Rendering Pipeline", "Crawler", "PreBuilding");
            return View();
        }

        #endregion
    }
}