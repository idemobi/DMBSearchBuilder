using Microsoft.Data.Sqlite;

namespace DMBSearchBuilder
{
    /// <summary>
    /// Manages the SQLite schema and writes generated page records for <see cref="DMBSearchBuilderAgent"/>.
    /// </summary>
    public static class DMBSearchDatabaseManager
    {
        /// <summary>
        /// Ensures that the DMB search database schema exists.
        /// </summary>
        /// <param name="databasePath">The SQLite database path to initialize.</param>
        public static void EnsureTableCreated(string databasePath)
        {
            string? directoryPath = Path.GetDirectoryName(Path.GetFullPath(databasePath));

            if (!string.IsNullOrWhiteSpace(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using SqliteConnection connection = new($"Data Source={databasePath}");
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = """
                                  CREATE TABLE IF NOT EXISTS SearchPages
                                  (
                                      Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                      Url TEXT NOT NULL UNIQUE,
                                      Title TEXT NOT NULL DEFAULT '',
                                      Description TEXT NOT NULL DEFAULT '',
                                      Keywords TEXT NOT NULL DEFAULT '',
                                      ContentLength INTEGER NOT NULL DEFAULT 0,
                                      CreatedUtc TEXT NOT NULL,
                                      UpdatedUtc TEXT NOT NULL
                                  );

                                  CREATE INDEX IF NOT EXISTS IX_SearchPages_Keywords
                                  ON SearchPages (Keywords);

                                  CREATE INDEX IF NOT EXISTS IX_SearchPages_Title
                                  ON SearchPages (Title);

                                  CREATE TABLE IF NOT EXISTS SearchPageTerms
                                  (
                                      Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                      PageId INTEGER NOT NULL,
                                      Term TEXT NOT NULL,
                                      Weight INTEGER NOT NULL DEFAULT 0,
                                      FOREIGN KEY(PageId) REFERENCES SearchPages(Id) ON DELETE CASCADE
                                  );

                                  CREATE UNIQUE INDEX IF NOT EXISTS IX_SearchPageTerms_Page_Term
                                  ON SearchPageTerms (PageId, Term);

                                  CREATE INDEX IF NOT EXISTS IX_SearchPageTerms_Term
                                  ON SearchPageTerms (Term);
                                  """;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Removes all generated pages from the target database.
        /// </summary>
        /// <param name="databasePath">The SQLite database path to clear.</param>
        public static void ClearPages(string databasePath)
        {
            EnsureTableCreated(databasePath);

            using SqliteConnection connection = new($"Data Source={databasePath}");
            connection.Open();

            using SqliteCommand command = connection.CreateCommand();
            command.CommandText = """
                                  DELETE FROM SearchPageTerms;
                                  DELETE FROM SearchPages;
                                  """;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Saves or updates one crawled page record.
        /// </summary>
        /// <param name="databasePath">The SQLite database path that receives the page.</param>
        /// <param name="record">The page record to save.</param>
        public static void SavePage(string databasePath, DMBSearchPageRecord record)
        {
            EnsureTableCreated(databasePath);

            string utcNow = DateTime.UtcNow.ToString("O");

            using SqliteConnection connection = new($"Data Source={databasePath}");
            connection.Open();
            using SqliteTransaction transaction = connection.BeginTransaction();

            using SqliteCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = """
                                  INSERT INTO SearchPages
                                  (Url, Title, Description, Keywords, ContentLength, CreatedUtc, UpdatedUtc)
                                  VALUES (@Url, @Title, @Description, @Keywords, @ContentLength, @CreatedUtc, @UpdatedUtc)
                                  ON CONFLICT(Url) DO UPDATE SET
                                      Title = excluded.Title,
                                      Description = excluded.Description,
                                      Keywords = excluded.Keywords,
                                      ContentLength = excluded.ContentLength,
                                      UpdatedUtc = excluded.UpdatedUtc;
                                  """;
            command.Parameters.AddWithValue("@Url", record.Url);
            command.Parameters.AddWithValue("@Title", record.Title);
            command.Parameters.AddWithValue("@Description", record.Description);
            command.Parameters.AddWithValue("@Keywords", record.Keywords);
            command.Parameters.AddWithValue("@ContentLength", record.ContentLength);
            command.Parameters.AddWithValue("@CreatedUtc", utcNow);
            command.Parameters.AddWithValue("@UpdatedUtc", utcNow);
            command.ExecuteNonQuery();

            using SqliteCommand pageIdCommand = connection.CreateCommand();
            pageIdCommand.Transaction = transaction;
            pageIdCommand.CommandText = "SELECT Id FROM SearchPages WHERE Url = @Url;";
            pageIdCommand.Parameters.AddWithValue("@Url", record.Url);
            long pageId = Convert.ToInt64(pageIdCommand.ExecuteScalar());

            using SqliteCommand deleteTermsCommand = connection.CreateCommand();
            deleteTermsCommand.Transaction = transaction;
            deleteTermsCommand.CommandText = "DELETE FROM SearchPageTerms WHERE PageId = @PageId;";
            deleteTermsCommand.Parameters.AddWithValue("@PageId", pageId);
            deleteTermsCommand.ExecuteNonQuery();

            foreach (KeyValuePair<string, int> weightedTerm in record.WeightedTerms)
            {
                using SqliteCommand termCommand = connection.CreateCommand();
                termCommand.Transaction = transaction;
                termCommand.CommandText = """
                                          INSERT INTO SearchPageTerms
                                          (PageId, Term, Weight)
                                          VALUES (@PageId, @Term, @Weight);
                                          """;
                termCommand.Parameters.AddWithValue("@PageId", pageId);
                termCommand.Parameters.AddWithValue("@Term", weightedTerm.Key);
                termCommand.Parameters.AddWithValue("@Weight", weightedTerm.Value);
                termCommand.ExecuteNonQuery();
            }

            transaction.Commit();
        }
    }
}
