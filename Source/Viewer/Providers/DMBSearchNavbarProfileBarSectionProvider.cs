#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.IO;
using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Injects the DMBSearchViewer search component into the BootstrapBuilder profile navbar area.
    /// </summary>
    public sealed class DMBSearchNavbarProfileBarSectionProvider : IProfileBarSectionProvider
    {
        #region Instance fields and properties

        #region From interface IProfileBarSectionProvider

        /// <summary>
        ///     Gets the provider ordering inside the navbar profile provider list.
        /// </summary>
        public int Order => 900;

        #endregion

        #endregion

        #region Instance methods

        #region From interface IProfileBarSectionProvider

        /// <summary>
        ///     Builds the profile navbar contribution for DMBSearchViewer.
        /// </summary>
        /// <param name="writer">The writer that receives rendered output.</param>
        /// <param name="html">The Razor HTML helper used to access view context.</param>
        /// <returns>The profile bar module result containing the search component.</returns>
        public ProfilBarModuleResult Build(TextWriter writer, IHtmlHelper html)
        {
            ProfilBarModuleResult result = new();
            result.NavbarComponents.Add(new DMBSearchNavbarComponent());
            return result;
        }

        /// <summary>
        ///     Indicates whether the search navbar component is enabled.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to access view context.</param>
        /// <returns><see langword="true" /> when the provider should be rendered.</returns>
        public bool IsEnabled(IHtmlHelper html)
        {
            return true;
        }

        #endregion

        #endregion
    }
}