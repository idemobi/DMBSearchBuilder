#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBDocumentationBuilderLabs.Controllers;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace DMBSearchBuilderLabs.Controllers
{
    /// <summary>
    ///     Provides documentation pages for the DMBSearchViewer package.
    /// </summary>
    public class SearchViewerController : RawBootstrapController
    {
        #region Static methods

        private static IconStruct ResolveBreadcrumbIcon(string actionName)
        {
            return actionName switch
            {
                nameof(GettingStarted) => IconStruct.Bootstrap("bi-play-circle"),
                nameof(Architecture) => IconStruct.Bootstrap("bi-diagram-3"),
                nameof(RenderingPipeline) => IconStruct.Bootstrap("bi-bezier2"),
                _ => IconStruct.Bootstrap("bi-info-circle")
            };
        }

        private static string ResolveBreadcrumbTitle(string actionName)
        {
            return actionName switch
            {
                nameof(GettingStarted) => "Getting Started",
                nameof(Architecture) => "Architecture",
                nameof(RenderingPipeline) => "Rendering Pipeline",
                _ => "Introduction"
            };
        }

        #endregion

        #region Instance methods

        private void AddInformationBreadcrumb(string? currentAction)
        {
            string actionName = string.IsNullOrWhiteSpace(currentAction) ? nameof(Introduction) : currentAction;

            AddBreadcrumb(
                new UrlActionItem().WithUrl("/").SetTitle("Home").SetIcon(IconStruct.BootstrapEnum(BootStrapEnum.bi_house)),
                new AspRouteActionItem("DocumentationHome", "Index").SetTitle("Documentation").SetIcon(IconStruct.BootstrapEnum(BootStrapEnum.bi_book)),
                new AspRouteActionItem("SearchViewer", nameof(Introduction)).SetTitle("SearchViewer").SetIcon(IconStruct.Bootstrap("bi-search-heart")),
                new AspRouteActionItem("SearchViewer", actionName).SetTitle(ResolveBreadcrumbTitle(actionName)).SetIcon(ResolveBreadcrumbIcon(actionName))
            );
        }

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
        ///     Configures the SearchViewer module sidebar before rendering an action.
        /// </summary>
        /// <param name="context">The current action execution context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            string? currentController = context.RouteData.Values["controller"]?.ToString();
            string? currentAction = context.RouteData.Values["action"]?.ToString();
            SetSidebar(DocumentationModuleSidebarAgent.CreateSidebar(currentController, currentAction));
            AddInformationBreadcrumb(currentAction);
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
