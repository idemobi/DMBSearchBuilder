#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBSearchBuilderLabs.Navigation;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace DMBSearchBuilderWebsite;

internal sealed class SearchBuilderWebsiteSidebarActionFilter : IActionFilter
{
    #region Instance methods

    #region From interface IActionFilter

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not RawBootstrapController controller)
        {
            return;
        }

        string? currentController = context.RouteData.Values["controller"]?.ToString();
        string? currentAction = context.RouteData.Values["action"]?.ToString();

        if (!SearchBuilderLabsNavigationAgent.IsModuleController(currentController))
        {
            return;
        }

        string moduleController = SearchBuilderLabsNavigationAgent.ResolveModuleController(currentController);
        string moduleAction = SearchBuilderLabsNavigationAgent.ResolveModuleDefaultAction(currentController);
        string actionName = string.IsNullOrWhiteSpace(currentAction) ? moduleAction : currentAction;

        controller.SetSidebar(SearchBuilderLabsNavigationAgent.CreateSidebar(currentController, currentAction));
        controller.AddBreadcrumb(
            ActionItemFactory.Url("Home", "/", IconStruct.Bootstrap("bi-house")),
            ActionItemFactory.AspRoute(moduleController, moduleAction)
                .SetTitle(SearchBuilderLabsNavigationAgent.ResolveModuleTitle(currentController))
                .SetIcon(SearchBuilderLabsNavigationAgent.ResolveModuleIcon(currentController)),
            ActionItemFactory.AspRoute(moduleController, actionName)
                .SetTitle(SearchBuilderLabsNavigationAgent.ResolveActionTitle(currentController, actionName))
                .SetIcon(SearchBuilderLabsNavigationAgent.ResolveActionIcon(currentController, actionName))
        );
    }

    #endregion

    #endregion
}
