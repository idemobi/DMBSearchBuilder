#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System.Collections.Generic;

namespace DMBSearchViewer
{
    /// <summary>
    ///     Contains merged search results and provider errors for one user query.
    /// </summary>
    public sealed class DMBSearchResponse
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the provider errors captured during execution.
        /// </summary>
        public List<DMBSearchProviderError> Errors { get; } = [];

        /// <summary>
        ///     Gets the merged search results.
        /// </summary>
        public List<DMBSearchResult> Results { get; } = [];

        #endregion
    }
}