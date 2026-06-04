#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Describes one provider error captured during a search operation.
    /// </summary>
    public sealed class DMBSearchProviderError
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the safe error message shown on the result page.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the provider display name.
        /// </summary>
        public string ProviderName { get; set; } = string.Empty;

        #endregion
    }
}