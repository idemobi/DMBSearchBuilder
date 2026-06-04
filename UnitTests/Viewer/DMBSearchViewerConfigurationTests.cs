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
public sealed class DMBSearchViewerConfigurationTests
{
    [Test]
    public void ConfigurationDoesNotRequireApiDescriptionOrExternalConfig()
    {
        DMBSearchViewerConfiguration configuration = new();

        Assert.Multiple(() =>
        {
            Assert.That(configuration.ApiDescription(), Is.False);
            Assert.That(configuration.NeedsConfigFileOrAppSettings(), Is.False);
        });
    }
}