#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchBuilder
{
    /// <summary>
    ///     Provides builder-level access to the shared <see cref="DMBSearchCore.SearchTextNormalizer" /> helpers.
    /// </summary>
    public static class SearchTextNormalizer
    {
        #region Static methods

        /// <summary>
        ///     Normalizes text and removes token separators to compare compact technical names.
        /// </summary>
        /// <param name="value">The text to compact.</param>
        /// <returns>A lowercase compact search string without accents, punctuation, or spaces.</returns>
        public static string CompactSearchText(string? value)
        {
            return DMBSearchCore.SearchTextNormalizer.CompactSearchText(value);
        }

        /// <summary>
        ///     Extracts normalized search tokens from text.
        /// </summary>
        /// <param name="value">The text to tokenize.</param>
        /// <returns>Ordered unique normalized tokens.</returns>
        public static IReadOnlyList<string> ExtractSearchTokens(string? value)
        {
            return DMBSearchCore.SearchTextNormalizer.ExtractSearchTokens(value);
        }

        /// <summary>
        ///     Normalizes text for storage or query matching.
        /// </summary>
        /// <param name="value">The text to normalize.</param>
        /// <returns>A lowercase ASCII-like search text without accents or special characters.</returns>
        public static string NormalizeSearchText(string? value)
        {
            return DMBSearchCore.SearchTextNormalizer.NormalizeSearchText(value);
        }

        #endregion
    }
}
