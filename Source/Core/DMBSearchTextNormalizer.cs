#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace DMBSearchCore
{
    /// <summary>
    ///     Normalizes search text so indexes and queries can share accent-insensitive, case-insensitive,
    ///     punctuation-insensitive, and compact technical-name matching rules.
    /// </summary>
    public static class DMBSearchTextNormalizer
    {
        #region Static fields and properties

        private static readonly Regex AcronymBoundaryRegex = new("([A-Z]+)([A-Z][a-z])", RegexOptions.Compiled);
        private static readonly Regex LowerUpperBoundaryRegex = new("([a-z0-9])([A-Z])", RegexOptions.Compiled);
        private static readonly Regex SearchTokenRegex = new("[a-z0-9]{2,}", RegexOptions.Compiled);
        private static readonly Regex WhitespaceRegex = new("\\s+", RegexOptions.Compiled);

        #endregion

        #region Static methods

        private static bool AppendKnownTransliteration(StringBuilder builder, char character)
        {
            switch (character)
            {
                case 'æ':
                    builder.Append("ae");
                    return true;
                case 'œ':
                    builder.Append("oe");
                    return true;
                case 'ß':
                    builder.Append("ss");
                    return true;
                case 'ø':
                    builder.Append('o');
                    return true;
                case 'đ':
                    builder.Append('d');
                    return true;
                case 'þ':
                    builder.Append("th");
                    return true;
                case 'ł':
                    builder.Append('l');
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        ///     Normalizes text and removes token separators to compare compact technical names.
        /// </summary>
        /// <param name="value">The text to compact.</param>
        /// <returns>A lowercase compact search string without accents, punctuation, or spaces.</returns>
        public static string CompactSearchText(string? value)
        {
            string normalized = NormalizeSearchText(value);
            return normalized.Replace(" ", string.Empty, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Extracts normalized search tokens from text.
        /// </summary>
        /// <param name="value">The text to tokenize.</param>
        /// <returns>Ordered unique normalized tokens.</returns>
        public static IReadOnlyList<string> ExtractSearchTokens(string? value)
        {
            string normalized = NormalizeSearchText(value);
            List<string> tokens = [];
            HashSet<string> knownTokens = new(StringComparer.Ordinal);

            foreach (Match match in SearchTokenRegex.Matches(normalized))
            {
                if (knownTokens.Add(match.Value))
                {
                    tokens.Add(match.Value);
                }
            }

            return tokens;
        }

        /// <summary>
        ///     Normalizes text for storage or query matching.
        /// </summary>
        /// <param name="value">The text to normalize.</param>
        /// <returns>A lowercase ASCII-like search text without accents or special characters.</returns>
        public static string NormalizeSearchText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            string decomposed = SplitWordBoundaries(value).Normalize(NormalizationForm.FormD);
            StringBuilder builder = new(decomposed.Length);

            foreach (char character in decomposed)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);

                if (category == UnicodeCategory.NonSpacingMark ||
                    category == UnicodeCategory.SpacingCombiningMark ||
                    category == UnicodeCategory.EnclosingMark)
                {
                    continue;
                }

                char lower = char.ToLowerInvariant(character);

                if (AppendKnownTransliteration(builder, lower))
                {
                    continue;
                }

                if (char.IsLetterOrDigit(lower))
                {
                    builder.Append(lower);
                }
                else
                {
                    builder.Append(' ');
                }
            }

            return WhitespaceRegex.Replace(builder.ToString().Normalize(NormalizationForm.FormC), " ").Trim();
        }

        private static string SplitWordBoundaries(string value)
        {
            string withAcronymBoundaries = AcronymBoundaryRegex.Replace(value, "$1 $2");
            return LowerUpperBoundaryRegex.Replace(withAcronymBoundaries, "$1 $2");
        }

        #endregion
    }
}