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

internal sealed class DMBSearchBuilderWebsiteSidebarActionFilter : IActionFilter
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

        if (!DMBSearchBuilderLabsNavigationAgent.IsModuleController(currentController))
        {
            return;
        }

        string moduleController = DMBSearchBuilderLabsNavigationAgent.ResolveModuleController(currentController);
        string moduleAction = DMBSearchBuilderLabsNavigationAgent.ResolveModuleDefaultAction(currentController);
        string actionName = string.IsNullOrWhiteSpace(currentAction) ? moduleAction : currentAction;

        controller.SetSidebar(DMBSearchBuilderLabsNavigationAgent.CreateSidebar(currentController, currentAction));
        controller.AddBreadcrumb(
            ActionItemFactory.Url("Home", "/", IconStruct.Bootstrap("bi-house")),
            ActionItemFactory.AspRoute(moduleController, moduleAction)
                .SetTitle(DMBSearchBuilderLabsNavigationAgent.ResolveModuleTitle(currentController))
                .SetIcon(DMBSearchBuilderLabsNavigationAgent.ResolveModuleIcon(currentController)),
            ActionItemFactory.AspRoute(moduleController, actionName)
                .SetTitle(DMBSearchBuilderLabsNavigationAgent.ResolveActionTitle(currentController, actionName))
                .SetIcon(DMBSearchBuilderLabsNavigationAgent.ResolveActionIcon(currentController, actionName))
        );
    }

    #endregion

    #endregion
}
