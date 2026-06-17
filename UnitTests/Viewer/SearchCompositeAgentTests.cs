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
public sealed class SearchCompositeAgentTests
{
    private sealed class StaticProvider(string name, params SearchResult[] results) : ISearchProvider
    {
        #region Instance fields and properties

        #region From interface ISearchProvider

        public string Name => name;

        #endregion

        #endregion

        #region Instance methods

        #region From interface ISearchProvider

        public Task<IReadOnlyList<SearchResult>> SearchAsync(SearchQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult<IReadOnlyList<SearchResult>>(results);
        }

        #endregion

        #endregion
    }

    private sealed class FailingProvider(string name) : ISearchProvider
    {
        #region Instance fields and properties

        #region From interface ISearchProvider

        public string Name => name;

        #endregion

        #endregion

        #region Instance methods

        #region From interface ISearchProvider

        public Task<IReadOnlyList<SearchResult>> SearchAsync(SearchQuery query, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("Provider failed.");
        }

        #endregion

        #endregion
    }

    private sealed class CancelingProvider : ISearchProvider
    {
        #region Instance fields and properties

        #region From interface ISearchProvider

        public string Name => "Canceling";

        #endregion

        #endregion

        #region Instance methods

        #region From interface ISearchProvider

        public Task<IReadOnlyList<SearchResult>> SearchAsync(SearchQuery query, CancellationToken cancellationToken)
        {
            throw new OperationCanceledException();
        }

        #endregion

        #endregion
    }

    [Test]
    public async Task SearchAsyncMergesResultsSortsByScoreAndCapturesProviderErrors()
    {
        ISearchProvider[] providers =
        [
            new StaticProvider("Low", new SearchResult { Title = "Low score", Score = 10 }),
            new FailingProvider("Failing"),
            new StaticProvider("High", new SearchResult { Title = "High score", Score = 95 })
        ];
        SearchCompositeAgent agent = new(providers);

        SearchResponse response = await agent.SearchAsync(new SearchQuery { Term = "search" }, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(response.Results.Select(result => result.Title), Is.EqualTo(new[] { "High score", "Low score" }));
            Assert.That(response.Errors, Has.Count.EqualTo(1));
            Assert.That(response.Errors[0].ProviderName, Is.EqualTo("Failing"));
            Assert.That(response.Errors[0].Message, Is.Not.Empty);
        });
    }

    [Test]
    public void SearchAsyncPropagatesCancellation()
    {
        SearchCompositeAgent agent = new([new CancelingProvider()]);

        Assert.That(
            async () => await agent.SearchAsync(new SearchQuery { Term = "search" }, CancellationToken.None),
            Throws.TypeOf<OperationCanceledException>());
    }
}