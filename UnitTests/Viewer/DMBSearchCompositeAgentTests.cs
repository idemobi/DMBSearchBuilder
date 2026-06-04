#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DMBSearchViewer;
using NUnit.Framework;

#endregion

namespace DMBSearchViewerUnitTest;

[TestFixture]
public sealed class DMBSearchCompositeAgentTests
{
    private sealed class StaticProvider(string name, params DMBSearchResult[] results) : IDMBSearchProvider
    {
        #region Instance fields and properties

        #region From interface IDMBSearchProvider

        public string Name => name;

        #endregion

        #endregion

        #region Instance methods

        #region From interface IDMBSearchProvider

        public Task<IReadOnlyList<DMBSearchResult>> SearchAsync(DMBSearchQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult<IReadOnlyList<DMBSearchResult>>(results);
        }

        #endregion

        #endregion
    }

    private sealed class FailingProvider(string name) : IDMBSearchProvider
    {
        #region Instance fields and properties

        #region From interface IDMBSearchProvider

        public string Name => name;

        #endregion

        #endregion

        #region Instance methods

        #region From interface IDMBSearchProvider

        public Task<IReadOnlyList<DMBSearchResult>> SearchAsync(DMBSearchQuery query, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("Provider failed.");
        }

        #endregion

        #endregion
    }

    private sealed class CancelingProvider : IDMBSearchProvider
    {
        #region Instance fields and properties

        #region From interface IDMBSearchProvider

        public string Name => "Canceling";

        #endregion

        #endregion

        #region Instance methods

        #region From interface IDMBSearchProvider

        public Task<IReadOnlyList<DMBSearchResult>> SearchAsync(DMBSearchQuery query, CancellationToken cancellationToken)
        {
            throw new OperationCanceledException();
        }

        #endregion

        #endregion
    }

    [Test]
    public async Task SearchAsyncMergesResultsSortsByScoreAndCapturesProviderErrors()
    {
        IDMBSearchProvider[] providers =
        [
            new StaticProvider("Low", new DMBSearchResult { Title = "Low score", Score = 10 }),
            new FailingProvider("Failing"),
            new StaticProvider("High", new DMBSearchResult { Title = "High score", Score = 95 })
        ];
        DMBSearchCompositeAgent agent = new(providers);

        DMBSearchResponse response = await agent.SearchAsync(new DMBSearchQuery { Term = "search" }, CancellationToken.None);

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
        DMBSearchCompositeAgent agent = new([new CancelingProvider()]);

        Assert.That(
            async () => await agent.SearchAsync(new DMBSearchQuery { Term = "search" }, CancellationToken.None),
            Throws.TypeOf<OperationCanceledException>());
    }
}