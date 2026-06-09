#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBSearchViewer;
using NUnit.Framework;

#endregion

namespace DMBSearchViewerUnitTest;

[TestFixture]
public sealed class DMBSearchPathResolverTests
{
    [Test]
    public void ResolveCombinesRelativePathWithContentRoot()
    {
        string contentRoot = Path.Combine(Path.GetTempPath(), "dmb-search-viewer-root");
        TestWebHostEnvironment environment = new() { ContentRootPath = contentRoot };

        string resolved = DMBSearchPathResolver.Resolve(environment, "Search/data.db");

        Assert.That(resolved, Is.EqualTo(Path.GetFullPath(Path.Combine(contentRoot, "Search/data.db"))));
    }

    [Test]
    public void ResolveKeepsAbsolutePath()
    {
        string absolutePath = Path.Combine(Path.GetTempPath(), "search.db");
        TestWebHostEnvironment environment = new() { ContentRootPath = "/content-root" };

        string resolved = DMBSearchPathResolver.Resolve(environment, absolutePath);

        Assert.That(resolved, Is.EqualTo(absolutePath));
    }
}