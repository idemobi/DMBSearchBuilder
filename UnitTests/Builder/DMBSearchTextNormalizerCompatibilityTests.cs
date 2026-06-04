#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using NUnit.Framework;
using BuilderNormalizer = DMBSearchBuilder.DMBSearchTextNormalizer;
using CoreNormalizer = DMBSearchCore.DMBSearchTextNormalizer;

#endregion

namespace DMBSearchBuilderUnitTest;

[TestFixture]
public sealed class DMBSearchTextNormalizerCompatibilityTests
{
    [Test]
    public void BuilderNormalizerDelegatesToCoreNormalizer()
    {
        const string value = "DéjàSearch URLParser";

        Assert.Multiple(() =>
        {
            Assert.That(BuilderNormalizer.NormalizeSearchText(value), Is.EqualTo(CoreNormalizer.NormalizeSearchText(value)));
            Assert.That(BuilderNormalizer.CompactSearchText(value), Is.EqualTo(CoreNormalizer.CompactSearchText(value)));
            Assert.That(BuilderNormalizer.ExtractSearchTokens(value), Is.EqualTo(CoreNormalizer.ExtractSearchTokens(value)));
        });
    }
}