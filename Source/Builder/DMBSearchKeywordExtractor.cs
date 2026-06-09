#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Net;
using System.Text.RegularExpressions;

#endregion

namespace DMBSearchBuilder
{
    /// <summary>
    ///     Extracts search-oriented metadata and keyword text from HTML pages.
    /// </summary>
    public static class DMBSearchKeywordExtractor
    {
        #region Static fields and properties

        private static readonly Regex H1Regex = new("<h1[^>]*>(.*?)</h1>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex H2H3Regex = new("<h[23][^>]*>(.*?)</h[23]>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex MetaDescriptionRegex = new("<meta[^>]+name=[\"']description[\"'][^>]+content=[\"']([^\"']*)[\"'][^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex ScriptAndStyleRegex = new("<(script|style)[^>]*>.*?</\\1>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex SearchTokenRegex = new("[a-z0-9]{2,}", RegexOptions.Compiled);
        private static readonly Regex TagRegex = new("<[^>]+>", RegexOptions.Compiled);
        private static readonly Regex TitleRegex = new("<title[^>]*>(.*?)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex WhitespaceRegex = new("\\s+", RegexOptions.Compiled);

        #endregion

        #region Static methods

        private static void AddCompactTerm(Dictionary<string, int> weightedTerms, string content, int weight)
        {
            IReadOnlyList<string> tokens = DMBSearchTextNormalizer.ExtractSearchTokens(content);

            if (tokens.Count < 2)
            {
                return;
            }

            string compactTerm = string.Concat(tokens);

            if (compactTerm.Length < 4)
            {
                return;
            }

            weightedTerms[compactTerm] = Math.Max(weightedTerms.GetValueOrDefault(compactTerm), weight * 8);
        }

        private static void AddWeightedTokens(Dictionary<string, int> weightedTerms, string content, int weight, int maxOccurrencesPerToken)
        {
            Dictionary<string, int> occurrences = CountTokenOccurrences(content);

            foreach (KeyValuePair<string, int> occurrence in occurrences)
            {
                int cappedOccurrences = Math.Min(occurrence.Value, maxOccurrencesPerToken);
                weightedTerms[occurrence.Key] = weightedTerms.GetValueOrDefault(occurrence.Key) + cappedOccurrences * weight;
            }
        }

        private static Dictionary<string, int> CountTokenOccurrences(string content)
        {
            string decodedContent = WebUtility.HtmlDecode(content) ?? string.Empty;
            string normalized = DMBSearchTextNormalizer.NormalizeSearchText(decodedContent);
            Dictionary<string, int> occurrences = new(StringComparer.Ordinal);

            foreach (Match match in SearchTokenRegex.Matches(normalized))
            {
                occurrences[match.Value] = occurrences.GetValueOrDefault(match.Value) + 1;
            }

            return occurrences;
        }

        /// <summary>
        ///     Extracts normalized keywords from a text or HTML fragment.
        /// </summary>
        /// <param name="content">The source content to tokenize.</param>
        /// <returns>A space-separated keyword string.</returns>
        public static string ExtractKeywordsAsString(string content)
        {
            string decodedContent = WebUtility.HtmlDecode(content) ?? string.Empty;
            return string.Join(' ', DMBSearchTextNormalizer.ExtractSearchTokens(decodedContent).Take(250));
        }

        /// <summary>
        ///     Extracts a page record from HTML content.
        /// </summary>
        /// <param name="url">The canonical page URL.</param>
        /// <param name="html">The HTML document content.</param>
        /// <returns>A normalized <see cref="DMBSearchPageRecord" /> ready for storage.</returns>
        public static DMBSearchPageRecord ExtractPage(string url, string html)
        {
            string title = NormalizeText(ReadFirstGroup(TitleRegex, html));
            string description = NormalizeText(ReadFirstGroup(MetaDescriptionRegex, html));
            string text = ExtractVisibleText(html);
            Dictionary<string, int> weightedTerms = ExtractWeightedTerms(url, html, title, description, text);

            if (string.IsNullOrWhiteSpace(description))
            {
                description = TrimForExcerpt(text, 220);
            }

            string keywords = ExtractKeywordsAsString($"{url} {title} {description} {text}");

            DMBSearchPageRecord record = new()
            {
                Url = url,
                Title = string.IsNullOrWhiteSpace(title) ? url : title,
                Description = description,
                Keywords = keywords,
                ContentLength = html.Length
            };

            foreach (KeyValuePair<string, int> weightedTerm in weightedTerms)
            {
                record.WeightedTerms[weightedTerm.Key] = weightedTerm.Value;
            }

            return record;
        }

        private static string ExtractVisibleText(string html)
        {
            string withoutScripts = ScriptAndStyleRegex.Replace(html, " ");
            string withoutTags = TagRegex.Replace(withoutScripts, " ");

            return NormalizeText(withoutTags);
        }

        private static Dictionary<string, int> ExtractWeightedTerms(string url, string html, string title, string description, string text)
        {
            Dictionary<string, int> weightedTerms = new(StringComparer.Ordinal);

            AddWeightedTokens(weightedTerms, url, 3, 3);
            AddWeightedTokens(weightedTerms, title, 10, 3);
            AddWeightedTokens(weightedTerms, description, 4, 4);
            AddWeightedTokens(weightedTerms, ReadAllGroups(H1Regex, html), 8, 3);
            AddWeightedTokens(weightedTerms, ReadAllGroups(H2H3Regex, html), 5, 3);
            AddWeightedTokens(weightedTerms, text, 1, 12);
            AddCompactTerm(weightedTerms, title, 10);
            AddCompactTerm(weightedTerms, ReadAllGroups(H1Regex, html), 8);
            AddCompactTerm(weightedTerms, ReadAllGroups(H2H3Regex, html), 5);

            return weightedTerms;
        }

        private static string NormalizeText(string value)
        {
            string decodedValue = WebUtility.HtmlDecode(value) ?? string.Empty;
            return WhitespaceRegex.Replace(decodedValue, " ").Trim();
        }

        private static string ReadAllGroups(Regex regex, string input)
        {
            return string.Join(' ', regex.Matches(input).Select(static match => match.Groups[1].Value));
        }

        private static string ReadFirstGroup(Regex regex, string input)
        {
            Match match = regex.Match(input);
            return match.Success && match.Groups.Count > 1 ? match.Groups[1].Value : string.Empty;
        }

        private static string TrimForExcerpt(string value, int maxLength)
        {
            if (value.Length <= maxLength)
            {
                return value;
            }

            return value[..maxLength].Trim();
        }

        #endregion
    }
}