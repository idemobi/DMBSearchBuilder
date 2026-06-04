#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBComponentBuilder;
using DMBEffectBuilder;
using DMBPageBuilder;
using DMBSearchBuilderLabs.Controllers;
using DMBSearchBuilderWebsite;
using DMBSearchViewer;
using DMBServerHelper;
using DMBServerWebHelper;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ServerHelperConfiguration.LoadCommonConfig(builder);
ServerHelperConfiguration.Config.CookiePrefix = "DSB";
ServerWebHelperConfiguration.LoadCommonConfig(builder);
PageBuilderConfiguration.LoadCommonConfig(builder);
BootstrapBuilderConfiguration.LoadCommonConfig(builder);
ComponentBuilderConfiguration.LoadCommonConfig(builder);
EffectBuilderConfiguration.LoadCommonConfig(builder);
DMBSearchViewerConfiguration.LoadCommonConfig(builder);

var mvcBuilder = builder.Services.AddControllersWithViews();
mvcBuilder.AddApplicationPart(typeof(SearchBuilderController).Assembly);
mvcBuilder.AddApplicationPart(typeof(SearchViewerController).Assembly);
mvcBuilder.AddApplicationPart(typeof(SearchController).Assembly);
mvcBuilder.AddMvcOptions(options => options.Filters.Add(new DMBSearchBuilderWebsiteSidebarActionFilter()));

builder.Services.AddTransient<IMenuBarSectionProvider, DMBSearchBuilderWebsiteMenuBarSectionProvider>();
builder.Services.AddTransient<IProfileBarSectionProvider, ThemeBarSectionProvider>();
builder.Services.AddTransient<IProfileBarSectionProvider, DebugBarSectionProvider>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

ServerWebHelperConfiguration.UseApp(app);
DMBSearchViewerConfiguration.UseApp(app);

app.MapGet("/", context =>
{
    context.Response.Redirect("/SearchBuilder/Introduction");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "search-index",
    pattern: "Search",
    defaults: new
    {
        controller = "Search",
        action = "Index"
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SearchBuilder}/{action=Introduction}/{id?}");

app.Run();
