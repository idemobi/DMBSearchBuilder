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
public sealed class DMBSearchModelTests
{
    [Test]
    public void OptionsAndQueryExposeStableDefaults()
    {
        DMBSearchViewerOptions options = new();
        DMBSearchQuery query = new();

        Assert.Multiple(() =>
        {
            Assert.That(options.SearchBuilderDatabasePath, Is.EqualTo("Search/data.db"));
            Assert.That(options.DocumentationDatabasePath, Is.EqualTo("Documentation/data.db"));
            Assert.That(options.MaxResultsPerProvider, Is.EqualTo(20));
            Assert.That(query.Term, Is.EqualTo(string.Empty));
            Assert.That(query.MaxResults, Is.EqualTo(20));
        });
    }

    [Test]
    public void ResponseAndPageViewModelKeepMutableResultAndErrorCollections()
    {
        DMBSearchResponse response = new();
        DMBSearchPageViewModel model = new() { Term = "search", IsDemo = true };

        response.Results.Add(new DMBSearchResult { Title = "Result", Score = 42 });
        response.Errors.Add(new DMBSearchProviderError { ProviderName = "Provider", Message = "Unavailable" });
        model.Results.AddRange(response.Results);
        model.Errors.AddRange(response.Errors);

        Assert.Multiple(() =>
        {
            Assert.That(model.Term, Is.EqualTo("search"));
            Assert.That(model.IsDemo, Is.True);
            Assert.That(model.Results, Has.Count.EqualTo(1));
            Assert.That(model.Errors, Has.Count.EqualTo(1));
            Assert.That(model.Results[0].Title, Is.EqualTo("Result"));
            Assert.That(model.Errors[0].ProviderName, Is.EqualTo("Provider"));
        });
    }
}