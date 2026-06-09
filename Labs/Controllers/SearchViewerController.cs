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
    ///     Provides documentation pages for the DMBSearchViewer package.
    /// </summary>
    public class SearchViewerController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the SearchViewer architecture page.
        /// </summary>
        /// <returns>The architecture view.</returns>
        public IActionResult Architecture()
        {
            SetTitle("SearchViewer - Architecture");
            SetDescription("SearchViewer architecture");
            SetKeywords("SearchViewer", "DMBSearchViewer", "Architecture", "Providers", "Navbar");
            return View();
        }

        /// <summary>
        ///     Renders the SearchViewer getting started page.
        /// </summary>
        /// <returns>The getting started view.</returns>
        public IActionResult GettingStarted()
        {
            SetTitle("SearchViewer - Getting Started");
            SetDescription("SearchViewer getting started guide");
            SetKeywords("SearchViewer", "DMBSearchViewer", "Getting Started", "Providers");
            return View();
        }

        /// <summary>
        ///     Renders the SearchViewer introduction page.
        /// </summary>
        /// <returns>The introduction view.</returns>
        public IActionResult Introduction()
        {
            SetTitle("SearchViewer - Introduction");
            SetDescription("SearchViewer");
            SetKeywords("SearchViewer", "DMBSearchViewer", "Search", "Navbar", "Providers");
            return View();
        }

        /// <summary>
        ///     Renders the SearchViewer rendering pipeline page.
        /// </summary>
        /// <returns>The rendering pipeline view.</returns>
        public IActionResult RenderingPipeline()
        {
            SetTitle("SearchViewer - Rendering Pipeline");
            SetDescription("SearchViewer rendering pipeline");
            SetKeywords("SearchViewer", "DMBSearchViewer", "Rendering Pipeline", "Providers", "Scores");
            return View();
        }

        #endregion
    }
}