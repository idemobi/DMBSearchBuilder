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
public sealed class DocumentationViewerSearchProviderTests
{
    private static DocumentationViewerSearchProvider CreateProvider(string databasePath, out string contentRoot)
    {
        contentRoot = Path.Combine(Path.GetTempPath(), $"dmb-doc-search-viewer-{Guid.NewGuid():N}");
        Directory.CreateDirectory(contentRoot);
        TestWebHostEnvironment environment = new() { ContentRootPath = contentRoot };
        return new DocumentationViewerSearchProvider(environment, Options.Create(new SearchViewerOptions
        {
            DocumentationDatabasePath = databasePath
        }));
    }

    private static void CreateDocumentationDatabase(string databasePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);
        using SqliteConnection connection = new($"Data Source={databasePath}");
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
                              CREATE TABLE DocumentationObjects
                              (
                                  PackageId TEXT,
                                  Version TEXT,
                                  NamespaceName TEXT,
                                  ObjectName TEXT,
                                  ObjectType TEXT,
                                  RoutePath TEXT,
                                  TechnicalKeywords TEXT,
                                  Keywords TEXT
                              );
                              INSERT INTO DocumentationObjects
                              (PackageId, Version, NamespaceName, ObjectName, ObjectType, RoutePath, TechnicalKeywords, Keywords)
                              VALUES
                              ('DMBSearchViewer', '0.12.0', 'DMBSearchViewer.Controllers', 'SearchController', 'class', '/Documentation/SearchController', 'search controller mvc', 'runtime search controller'),
                              ('DMBSearchViewer', '0.12.0', '<global namespace>', '<global namespace>', 'namespace', '/Documentation/Global', 'global', 'global');
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
    public async Task SearchAsyncReadsDocumentationDatabaseScoresAndFiltersGlobalNamespaceRows()
    {
        DocumentationViewerSearchProvider provider = CreateProvider("Documentation/data.db", out string contentRoot);
        string databasePath = Path.Combine(contentRoot, "Documentation", "data.db");

        try
        {
            CreateDocumentationDatabase(databasePath);

            IReadOnlyList<SearchResult> results = await provider.SearchAsync(new SearchQuery
            {
                Term = "SearchController",
                MaxResults = 5
            }, CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(results, Has.Count.EqualTo(1));
                Assert.That(results[0].SourceName, Is.EqualTo("DMBDocumentationViewer"));
                Assert.That(results[0].Title, Is.EqualTo("SearchController (class)"));
                Assert.That(results[0].Url, Is.EqualTo("/Documentation/SearchController"));
                Assert.That(results[0].Excerpt, Is.EqualTo("DMBSearchViewer 0.12.0 DMBSearchViewer.Controllers"));
                Assert.That(results[0].Score, Is.GreaterThan(0));
            });
        }
        finally
        {
            DeleteDirectory(contentRoot);
        }
    }

    [Test]
    public async Task SearchAsyncReturnsEmptyForMissingDatabaseOrBlankTerm()
    {
        DocumentationViewerSearchProvider provider = CreateProvider("missing.db", out string contentRoot);

        try
        {
            IReadOnlyList<SearchResult> missingDatabaseResults = await provider.SearchAsync(new SearchQuery { Term = "SearchController" }, CancellationToken.None);
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
}