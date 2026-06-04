#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBSearchBuilder;

#endregion

string moduleRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../.."));
string websiteRoot = Path.Combine(moduleRoot, "Website");
string searchDirectory = Path.Combine(websiteRoot, "Search");
string databasePath = Path.Combine(searchDirectory, "data.db");

Directory.CreateDirectory(searchDirectory);

DMBSearchLaunchProfileBuildOptions options = new()
{
    ProjectPath = Path.Combine(websiteRoot, "DMBSearchBuilderWebsite.csproj"),
    LaunchSettingsPath = Path.Combine(websiteRoot, "Properties", "launchSettings.json"),
    LaunchProfileName = "https",
    FallbackLaunchProfileName = "https",
    DatabasePath = databasePath,
    MaxPages = 150,
    PreserveQueryStrings = false,
    UseNoBuild = false
};

await new DMBSearchBuilderAgent().BuildFromLaunchProfileAsync(options);
