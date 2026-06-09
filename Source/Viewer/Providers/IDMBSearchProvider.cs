#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Defines a DMBSearchViewer source capable of searching one database or index.
    /// </summary>
    public interface IDMBSearchProvider
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the provider display name.
        /// </summary>
        string Name { get; }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Searches the provider source.
        /// </summary>
        /// <param name="query">The search query to execute.</param>
        /// <param name="cancellationToken">A token used to cancel the provider query.</param>
        /// <returns>A list of normalized search results.</returns>
        Task<IReadOnlyList<DMBSearchResult>> SearchAsync(DMBSearchQuery query, CancellationToken cancellationToken);

        #endregion
    }
}