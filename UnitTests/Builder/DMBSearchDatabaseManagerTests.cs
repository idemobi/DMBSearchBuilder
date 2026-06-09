#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBSearchBuilder;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

#endregion

namespace DMBSearchBuilderUnitTest;

[TestFixture]
public sealed class DMBSearchDatabaseManagerTests
{
    private static string CreateTemporaryDatabasePath()
    {
        return Path.Combine(Path.GetTempPath(), $"dmb-search-builder-{Guid.NewGuid():N}.db");
    }

    private static bool TableExists(string databasePath, string tableName)
    {
        using SqliteConnection connection = new($"Data Source={databasePath}");
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = @TableName;";
        command.Parameters.AddWithValue("@TableName", tableName);
        return Convert.ToInt64(command.ExecuteScalar()) == 1;
    }

    private static T Scalar<T>(string databasePath, string commandText)
    {
        using SqliteConnection connection = new($"Data Source={databasePath}");
        connection.Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = commandText;
        return (T)Convert.ChangeType(command.ExecuteScalar()!, typeof(T));
    }

    private static void DeleteDatabase(string databasePath)
    {
        if (File.Exists(databasePath))
        {
            File.Delete(databasePath);
        }
    }

    [Test]
    public void ClearPagesRemovesPagesAndTerms()
    {
        string databasePath = CreateTemporaryDatabasePath();

        try
        {
            DMBSearchPageRecord record = new()
            {
                Url = "https://example.com/clear",
                Title = "Clear page",
                Keywords = "clear page"
            };
            record.WeightedTerms["clear"] = 5;

            DMBSearchDatabaseManager.SavePage(databasePath, record);
            DMBSearchDatabaseManager.ClearPages(databasePath);

            Assert.Multiple(() =>
            {
                Assert.That(Scalar<long>(databasePath, "SELECT COUNT(*) FROM SearchPages;"), Is.EqualTo(0L));
                Assert.That(Scalar<long>(databasePath, "SELECT COUNT(*) FROM SearchPageTerms;"), Is.EqualTo(0L));
            });
        }
        finally
        {
            DeleteDatabase(databasePath);
        }
    }

    [Test]
    public void EnsureTableCreatedCreatesExpectedTables()
    {
        string databasePath = CreateTemporaryDatabasePath();

        try
        {
            DMBSearchDatabaseManager.EnsureTableCreated(databasePath);

            Assert.Multiple(() =>
            {
                Assert.That(TableExists(databasePath, "SearchPages"), Is.True);
                Assert.That(TableExists(databasePath, "SearchPageTerms"), Is.True);
            });
        }
        finally
        {
            DeleteDatabase(databasePath);
        }
    }

    [Test]
    public void SavePageInsertsAndUpdatesSinglePageWithWeightedTerms()
    {
        string databasePath = CreateTemporaryDatabasePath();

        try
        {
            DMBSearchPageRecord firstRecord = new()
            {
                Url = "https://example.com/search",
                Title = "Search page",
                Description = "First description",
                Keywords = "search first",
                ContentLength = 10
            };
            firstRecord.WeightedTerms["search"] = 12;
            firstRecord.WeightedTerms["first"] = 4;

            DMBSearchDatabaseManager.SavePage(databasePath, firstRecord);

            DMBSearchPageRecord updatedRecord = new()
            {
                Url = "https://example.com/search",
                Title = "Updated search page",
                Description = "Updated description",
                Keywords = "search updated",
                ContentLength = 20
            };
            updatedRecord.WeightedTerms["search"] = 15;
            updatedRecord.WeightedTerms["updated"] = 6;

            DMBSearchDatabaseManager.SavePage(databasePath, updatedRecord);

            Assert.Multiple(() =>
            {
                Assert.That(Scalar<long>(databasePath, "SELECT COUNT(*) FROM SearchPages;"), Is.EqualTo(1L));
                Assert.That(Scalar<string>(databasePath, "SELECT Title FROM SearchPages WHERE Url = 'https://example.com/search';"), Is.EqualTo("Updated search page"));
                Assert.That(Scalar<long>(databasePath, "SELECT COUNT(*) FROM SearchPageTerms;"), Is.EqualTo(2L));
                Assert.That(Scalar<long>(databasePath, "SELECT COUNT(*) FROM SearchPageTerms WHERE Term = 'first';"), Is.EqualTo(0L));
                Assert.That(Scalar<long>(databasePath, "SELECT Weight FROM SearchPageTerms WHERE Term = 'updated';"), Is.EqualTo(6L));
            });
        }
        finally
        {
            DeleteDatabase(databasePath);
        }
    }
}