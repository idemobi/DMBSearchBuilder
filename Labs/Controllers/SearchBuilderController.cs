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
    ///     Provides documentation pages for the DMBSearchBuilder package.
    /// </summary>
    public class SearchBuilderController : RawBootstrapController
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
                new AspRouteActionItem("SearchBuilder", nameof(Introduction)).SetTitle("SearchBuilder").SetIcon(IconStruct.Bootstrap("bi-search")),
                new AspRouteActionItem("SearchBuilder", actionName).SetTitle(ResolveBreadcrumbTitle(actionName)).SetIcon(ResolveBreadcrumbIcon(actionName))
            );
        }

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
        ///     Configures the SearchBuilder module sidebar before rendering an action.
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
