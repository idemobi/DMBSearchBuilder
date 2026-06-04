#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Collections.Generic;
using DMBSearchCore;
using NUnit.Framework;

#endregion

namespace DMBSearchCoreUnitTest;

[TestFixture]
public sealed class DMBSearchTextNormalizerTests
{
    [Test]
    public void CompactSearchTextRemovesSeparatorsAfterNormalization()
    {
        string compact = DMBSearchTextNormalizer.CompactSearchText("Search-URL Parser");

        Assert.That(compact, Is.EqualTo("searchurlparser"));
    }

    [Test]
    public void ExtractSearchTokensReturnsOrderedUniqueTokensWithMinimumLength()
    {
        IReadOnlyList<string> tokens = DMBSearchTextNormalizer.ExtractSearchTokens("A C# search search SQL v2 endpoint");

        Assert.That(tokens, Is.EqualTo(new[] { "search", "sql", "v2", "endpoint" }));
    }

    [Test]
    public void NormalizeSearchTextHandlesNullEmptyAccentsTransliterationAndPunctuation()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DMBSearchTextNormalizer.NormalizeSearchText(null), Is.EqualTo(string.Empty));
            Assert.That(DMBSearchTextNormalizer.NormalizeSearchText("   "), Is.EqualTo(string.Empty));
            Assert.That(DMBSearchTextNormalizer.NormalizeSearchText("Déjà-vu, Æther œuf Straße!"), Is.EqualTo("deja vu aether oeuf strasse"));
            Assert.That(DMBSearchTextNormalizer.NormalizeSearchText("smørrebrød þorn Đelta Łodz"), Is.EqualTo("smorrebrod thorn delta lodz"));
        });
    }

    [Test]
    public void NormalizeSearchTextSplitsCamelCaseAndAcronymBoundaries()
    {
        string normalized = DMBSearchTextNormalizer.NormalizeSearchText("JSONParser HTMLPage SearchURL2Value");

        Assert.That(normalized, Is.EqualTo("json parser html page search url2 value"));
    }
}