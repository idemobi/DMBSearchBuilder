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
public sealed class DMBSearchKeywordExtractorTests
{
    [Test]
    public void ExtractKeywordsAsStringDecodesHtmlAndKeepsUniqueNormalizedTokens()
    {
        string keywords = DMBSearchKeywordExtractor.ExtractKeywordsAsString("Caf&eacute; search search URLParser &amp; C#");

        Assert.That(keywords, Is.EqualTo("cafe search url parser"));
    }

    [Test]
    public void ExtractPageFallsBackToVisibleTextWhenDescriptionIsMissing()
    {
        string html = $"<html><head><title>Fallback</title></head><body><p>{new string('a', 250)}</p></body></html>";

        DMBSearchPageRecord record = DMBSearchKeywordExtractor.ExtractPage("https://example.com/fallback", html);

        Assert.Multiple(() =>
        {
            Assert.That(record.Description, Has.Length.LessThanOrEqualTo(220));
            Assert.That(record.Description, Does.StartWith("Fallback "));
            Assert.That(record.Description, Does.Contain(new string('a', 20)));
        });
    }

    [Test]
    public void ExtractPageReadsMetadataVisibleTextAndWeightedTerms()
    {
        const string html = """
                            <html>
                            <head>
                                <title>Search URL Parser</title>
                                <meta name="description" content="Find caf&eacute; pages quickly">
                                <script>ignoredScriptTerm()</script>
                                <style>.ignoredStyleTerm { color: red; }</style>
                            </head>
                            <body>
                                <h1>Search URL Parser</h1>
                                <h2>Fast lookup</h2>
                                <p>Visible body content for café search.</p>
                            </body>
                            </html>
                            """;

        DMBSearchPageRecord record = DMBSearchKeywordExtractor.ExtractPage("https://example.com/search-url-parser", html);

        Assert.Multiple(() =>
        {
            Assert.That(record.Url, Is.EqualTo("https://example.com/search-url-parser"));
            Assert.That(record.Title, Is.EqualTo("Search URL Parser"));
            Assert.That(record.Description, Is.EqualTo("Find café pages quickly"));
            Assert.That(record.ContentLength, Is.EqualTo(html.Length));
            Assert.That(record.Keywords, Does.Contain("search"));
            Assert.That(record.Keywords, Does.Contain("cafe"));
            Assert.That(record.Keywords, Does.Not.Contain("ignoredscriptterm"));
            Assert.That(record.WeightedTerms["search"], Is.GreaterThan(record.WeightedTerms["body"]));
            Assert.That(record.WeightedTerms["searchurlparser"], Is.GreaterThan(0));
        });
    }
}