#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBSearchBuilder;
using NUnit.Framework;

#endregion

namespace DMBSearchBuilderUnitTest;

[TestFixture]
public sealed class DMBSearchOptionsTests
{
    [Test]
    public void BuildOptionsExposeStableDefaults()
    {
        DMBSearchBuildOptions options = new();

        Assert.Multiple(() =>
        {
            Assert.That(options.BaseUri, Is.EqualTo(new Uri("http://localhost:5000/")));
            Assert.That(options.DatabasePath, Is.EqualTo("Search/data.db"));
            Assert.That(options.MaxPages, Is.EqualTo(500));
            Assert.That(options.RequestTimeout, Is.EqualTo(TimeSpan.FromSeconds(10)));
            Assert.That(options.ExcludedPathPrefixes, Is.EqualTo(new[] { "/Documentation", "/Search" }));
            Assert.That(options.PreserveQueryStrings, Is.False);
            Assert.That(options.UserAgent, Is.EqualTo("DMBSearchBuilder/0.9"));
        });
    }

    [Test]
    public void LaunchProfileBuildOptionsExposeStableDefaults()
    {
        DMBSearchLaunchProfileBuildOptions options = new();

        Assert.Multiple(() =>
        {
            Assert.That(options.ProjectPath, Is.EqualTo(string.Empty));
            Assert.That(options.LaunchSettingsPath, Is.EqualTo(string.Empty));
            Assert.That(options.LaunchProfileName, Is.EqualTo("https"));
            Assert.That(options.FallbackLaunchProfileName, Is.Null);
            Assert.That(options.DatabasePath, Is.EqualTo("Search/data.db"));
            Assert.That(options.MaxPages, Is.EqualTo(500));
            Assert.That(options.RequestTimeout, Is.EqualTo(TimeSpan.FromSeconds(10)));
            Assert.That(options.StartupTimeout, Is.EqualTo(TimeSpan.FromSeconds(90)));
            Assert.That(options.UseNoBuild, Is.True);
            Assert.That(options.ExcludedPathPrefixes, Is.EqualTo(new[] { "/Documentation", "/Search" }));
            Assert.That(options.PreserveQueryStrings, Is.False);
            Assert.That(options.UserAgent, Is.EqualTo("DMBSearchBuilder/0.9"));
        });
    }
}