#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBSearchViewer;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using NUnit.Framework;

#endregion

namespace DMBSearchViewerUnitTest;

[TestFixture]
public sealed class SearchBuilderSearchProviderTests
{
    private static SearchBuilderSearchProvider CreateProvider(string databasePath, out string contentRoot)
    {
        contentRoot = Path.Combine(Path.GetTempPath(), $"dmb-search-viewer-{Guid.NewGuid():N}");
        Directory.CreateDirectory(contentRoot);
        TestWebHostEnvironment environment = new() { ContentRootPath = contentRoot };
        return new SearchBuilderSearchProvider(environment, Options.Create(new SearchViewerOptions
        {
            SearchBuilderDatabasePath = databasePath
        }));
    }

    private static void CreateWeightedSearchDatabase(string databasePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);
        using SqliteConnection connection = new($"Data Source={databasePath}");
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
                              CREATE TABLE SearchPages
                              (
                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                  Url TEXT NOT NULL UNIQUE,
                                  Title TEXT NOT NULL,
                                  Description TEXT NOT NULL,
                                  Keywords TEXT NOT NULL
                              );
                              CREATE TABLE SearchPageTerms
                              (
                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                  PageId INTEGER NOT NULL,
                                  Term TEXT NOT NULL,
                                  Weight INTEGER NOT NULL
                              );
                              INSERT INTO SearchPages (Url, Title, Description, Keywords)
                              VALUES ('http://localhost:5000/docs/search-url', 'Search URL Parser', 'Search URL documentation', 'search url parser');
                              INSERT INTO SearchPageTerms (PageId, Term, Weight)
                              VALUES (1, 'search', 30), (1, 'url', 20);
                              """;
        command.ExecuteNonQuery();
    }

    private static void CreateLegacySearchDatabase(string databasePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);
        using SqliteConnection connection = new($"Data Source={databasePath}");
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
                              CREATE TABLE SearchPages
                              (
                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                  Url TEXT NOT NULL UNIQUE,
                                  Title TEXT NOT NULL,
                                  Description TEXT NOT NULL,
                                  Keywords TEXT NOT NULL
                              );
                              INSERT INTO SearchPages (Url, Title, Description, Keywords)
                              VALUES ('/legacy/search-url', 'Legacy Search URL', 'Legacy index result', 'search url parser');
                              """;
        command.ExecuteNonQuery();
    }

    private static void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, recursive: true);
        }
    }

    [Test]
    public async Task SearchAsyncFallsBackToLegacyKeywordIndex()
    {
        SearchBuilderSearchProvider provider = CreateProvider("Search/data.db", out string contentRoot);
        string databasePath = Path.Combine(contentRoot, "Search", "data.db");

        try
        {
            CreateLegacySearchDatabase(databasePath);

            IReadOnlyList<SearchResult> results = await provider.SearchAsync(new SearchQuery
            {
                Term = "SearchURL",
                MaxResults = 10
            }, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(results, Has.Count.EqualTo(1));
                Assert.That(results[0].Title, Is.EqualTo("Legacy Search URL"));
                Assert.That(results[0].Url, Is.EqualTo("/legacy/search-url"));
            });
        }
        finally
        {
            DeleteDirectory(contentRoot);
        }
    }

    [Test]
    public async Task SearchAsyncReturnsEmptyWhenDatabaseIsMissingOrTermIsBlank()
    {
        SearchBuilderSearchProvider provider = CreateProvider("missing.db", out string contentRoot);

        try
        {
            IReadOnlyList<SearchResult> missingDatabaseResults = await provider.SearchAsync(new SearchQuery { Term = "search" }, CancellationToken.None);
            IReadOnlyList<SearchResult> blankTermResults = await provider.SearchAsync(new SearchQuery { Term = " " }, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(missingDatabaseResults, Is.Empty);
                Assert.That(blankTermResults, Is.Empty);
            });
        }
        finally
        {
            DeleteDirectory(contentRoot);
        }
    }

    [Test]
    public async Task SearchAsyncUsesWeightedIndexAndNormalizesLocalhostUrls()
    {
        SearchBuilderSearchProvider provider = CreateProvider("Search/data.db", out string contentRoot);
        string databasePath = Path.Combine(contentRoot, "Search", "data.db");

        try
        {
            CreateWeightedSearchDatabase(databasePath);

            IReadOnlyList<SearchResult> results = await provider.SearchAsync(new SearchQuery
            {
                Term = "Search URL",
                MaxResults = 10
            }, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(results, Has.Count.EqualTo(1));
                Assert.That(results[0].SourceName, Is.EqualTo("DMBSearchBuilder"));
                Assert.That(results[0].Title, Is.EqualTo("Search URL Parser"));
                Assert.That(results[0].Url, Is.EqualTo("/docs/search-url"));
                Assert.That(results[0].Score, Is.GreaterThan(0));
            });
        }
        finally
        {
            DeleteDirectory(contentRoot);
        }
    }
}