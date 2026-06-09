#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBSearchBuilderLabs.Navigation;

/// <summary>
///     Provides reusable navigation fragments for DMBSearchBuilder labs hosts.
/// </summary>
/// <remarks>
///     The agent only builds Search-specific menu and sidebar fragments. Host websites remain responsible
///     for assembling these fragments into their navbar providers, sidebar filters, and global navigation.
/// </remarks>
public static class DMBSearchBuilderLabsNavigationAgent
{
    #region Static methods

    /// <summary>
    ///     Creates an action item for a DMBSearchBuilder labs or viewer page.
    /// </summary>
    /// <param name="controller">The MVC controller name used by the route.</param>
    /// <param name="action">The MVC action name used by the route.</param>
    /// <param name="title">The action title shown in navigation UI.</param>
    /// <param name="icon">The Bootstrap Icons CSS class used by the action.</param>
    /// <param name="currentController">The current MVC controller name used to mark the action active.</param>
    /// <param name="currentAction">The current MVC action name used to mark the action active.</param>
    /// <returns>The configured <see cref="AspRouteActionItem" />.</returns>
    public static AspRouteActionItem CreateAction(
        string controller,
        string action,
        string title,
        string icon,
        string? currentController = null,
        string? currentAction = null
    )
    {
        bool active =
            string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase);

        return ActionItemFactory.AspRoute(controller, action)
            .SetTitle(title)
            .SetIcon(IconStruct.Bootstrap(icon))
            .SetActive(active);
    }

    /// <summary>
    ///     Creates the DMBSearchBuilder navbar menu group.
    /// </summary>
    /// <returns>The configured <see cref="GroupActionItem" /> containing Search labs and viewer links.</returns>
    public static GroupActionItem CreateMenuGroup()
    {
        return ActionItemFactory.Group("DMB Search", IconStruct.Bootstrap("bi-search"))
            .AddItems(
                CreateAction("SearchBuilder", "Introduction", "DMBSearchBuilder", "bi-tools"),
                CreateAction("SearchViewer", "Introduction", "DMBSearchViewer", "bi-window-sidebar"),
                CreateAction("Search", "Index", "DMBSearchViewer Runtime", "bi-search-heart"),
                CreateAction("Search", "Demo", "DMBSearch Demo", "bi-stars")
            );
    }

    /// <summary>
    ///     Creates the local DMBSearchViewer runtime sidebar section.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <returns>The configured <see cref="SideBarSectionComponent" />.</returns>
    public static SideBarSectionComponent CreateSearchSidebarSection(string? currentController, string? currentAction)
    {
        return new SideBarSectionComponent("Local Search")
            .Add(ActionItemFactory.Group("Viewer Routes", IconStruct.Bootstrap("bi-search-heart"))
                .AddItems(
                    CreateAction("Search", "Index", "Search", "bi-search", currentController, currentAction),
                    CreateAction("Search", "Demo", "Demo", "bi-stars", currentController, currentAction)
                ));
    }

    /// <summary>
    ///     Creates the complete DMBSearchBuilder sidebar component.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <param name="sidebarId">The HTML identifier applied to the sidebar component.</param>
    /// <param name="localStorageKey">The browser local-storage key used for sidebar state.</param>
    /// <returns>The configured <see cref="SideBarComponent" />.</returns>
    public static SideBarComponent CreateSidebar(
        string? currentController,
        string? currentAction,
        string sidebarId = "search_sidebar",
        string localStorageKey = "dmbsearchbuilder.labs.sidebar"
    )
    {
        SideBarComponent sidebar = new SideBarComponent()
            .WithId(sidebarId)
            .WithLocalStorageKey(localStorageKey)
            .WithAutoExpandActivePath()
            .WithRememberExpandedState();

        sidebar.AddSection(CreateSidebarSection(currentController, currentAction));
        sidebar.AddSection(CreateViewerSidebarSection(currentController, currentAction));
        sidebar.AddSection(CreateSearchSidebarSection(currentController, currentAction));

        return sidebar;
    }

    /// <summary>
    ///     Creates the DMBSearchBuilder labs sidebar section.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <returns>The configured <see cref="SideBarSectionComponent" />.</returns>
    public static SideBarSectionComponent CreateSidebarSection(string? currentController, string? currentAction)
    {
        return new SideBarSectionComponent("DMBSearchBuilder")
            .Add(ActionItemFactory.Group("Builder", IconStruct.Bootstrap("bi-tools"))
                .AddItems(
                    CreateAction("SearchBuilder", "Introduction", "Introduction", "bi-info-circle", currentController, currentAction),
                    CreateAction("SearchBuilder", "GettingStarted", "Getting Started", "bi-play-circle", currentController, currentAction),
                    CreateAction("SearchBuilder", "Architecture", "Architecture", "bi-diagram-3", currentController, currentAction),
                    CreateAction("SearchBuilder", "RenderingPipeline", "Rendering Pipeline", "bi-bezier2", currentController, currentAction)
                ));
    }

    /// <summary>
    ///     Creates the DMBSearchViewer labs sidebar section.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <returns>The configured <see cref="SideBarSectionComponent" />.</returns>
    public static SideBarSectionComponent CreateViewerSidebarSection(string? currentController, string? currentAction)
    {
        return new SideBarSectionComponent("DMBSearchViewer")
            .Add(ActionItemFactory.Group("Viewer", IconStruct.Bootstrap("bi-window-sidebar"))
                .AddItems(
                    CreateAction("SearchViewer", "Introduction", "Introduction", "bi-info-circle", currentController, currentAction),
                    CreateAction("SearchViewer", "GettingStarted", "Getting Started", "bi-play-circle", currentController, currentAction),
                    CreateAction("SearchViewer", "Architecture", "Architecture", "bi-diagram-3", currentController, currentAction),
                    CreateAction("SearchViewer", "RenderingPipeline", "Rendering Pipeline", "bi-bezier2", currentController, currentAction)
                ));
    }

    /// <summary>
    ///     Determines whether a controller belongs to the DMBSearchBuilder labs navigation.
    /// </summary>
    /// <param name="controller">The MVC controller name to inspect.</param>
    /// <returns><c>true</c> when the controller is part of the Search module; otherwise, <c>false</c>.</returns>
    public static bool IsModuleController(string? controller)
    {
        return string.Equals(controller, "SearchBuilder", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(controller, "SearchViewer", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for a DMBSearchBuilder labs action.
    /// </summary>
    /// <param name="controller">The MVC controller name to resolve.</param>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The icon value represented as an <see cref="IconStruct" />.</returns>
    public static IconStruct ResolveActionIcon(string? controller, string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => IconStruct.Bootstrap("bi-play-circle"),
            "Architecture" => IconStruct.Bootstrap("bi-diagram-3"),
            "RenderingPipeline" => IconStruct.Bootstrap("bi-bezier2"),
            "Demo" => IconStruct.Bootstrap("bi-stars"),
            "Index" when string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase) => IconStruct.Bootstrap("bi-search"),
            _ => IconStruct.Bootstrap("bi-info-circle")
        };
    }

    /// <summary>
    ///     Resolves the display title for a DMBSearchBuilder labs action.
    /// </summary>
    /// <param name="controller">The MVC controller name to resolve.</param>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The display title for the action.</returns>
    public static string ResolveActionTitle(string? controller, string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => "Getting Started",
            "Architecture" => "Architecture",
            "RenderingPipeline" => "Rendering Pipeline",
            "Demo" => "Demo",
            "Index" when string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase) => "Search",
            _ => "Introduction"
        };
    }

    /// <summary>
    ///     Resolves the default controller for a DMBSearchBuilder labs route.
    /// </summary>
    /// <param name="controller">The MVC controller name to resolve.</param>
    /// <returns>The normalized controller name used for routing.</returns>
    public static string ResolveModuleController(string? controller)
    {
        return string.Equals(controller, "SearchViewer", StringComparison.OrdinalIgnoreCase) ? "SearchViewer" :
            string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase) ? "Search" :
            "SearchBuilder";
    }

    /// <summary>
    ///     Resolves the default action for a DMBSearchBuilder labs controller.
    /// </summary>
    /// <param name="controller">The MVC controller name to resolve.</param>
    /// <returns>The default action name for the controller.</returns>
    public static string ResolveModuleDefaultAction(string? controller)
    {
        return string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase) ? "Index" : "Introduction";
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for a DMBSearchBuilder labs controller.
    /// </summary>
    /// <param name="controller">The MVC controller name to resolve.</param>
    /// <returns>The icon value represented as an <see cref="IconStruct" />.</returns>
    public static IconStruct ResolveModuleIcon(string? controller)
    {
        return string.Equals(controller, "SearchViewer", StringComparison.OrdinalIgnoreCase) ? IconStruct.Bootstrap("bi-window-sidebar") :
            string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase) ? IconStruct.Bootstrap("bi-search-heart") :
            IconStruct.Bootstrap("bi-tools");
    }

    /// <summary>
    ///     Resolves the display title for a DMBSearchBuilder labs controller.
    /// </summary>
    /// <param name="controller">The MVC controller name to resolve.</param>
    /// <returns>The display title for the controller.</returns>
    public static string ResolveModuleTitle(string? controller)
    {
        return string.Equals(controller, "SearchViewer", StringComparison.OrdinalIgnoreCase) ? "DMBSearchViewer" :
            string.Equals(controller, "Search", StringComparison.OrdinalIgnoreCase) ? "DMBSearchViewer Runtime" :
            "DMBSearchBuilder";
    }

    #endregion
}
