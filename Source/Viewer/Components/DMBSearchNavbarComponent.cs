#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Net;
using DMBBootstrapBuilder;
using DMBServerHelper;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Renders the DMBSearchViewer navbar search form.
    /// </summary>
    public sealed class DMBSearchNavbarComponent : INavbarComponent
    {
        #region Instance methods

        #region From interface INavbarComponent

        /// <summary>
        ///     Renders the navbar search form.
        /// </summary>
        /// <param name="htmlHelper">The Razor HTML helper used to access the current request.</param>
        /// <returns>The rendered navbar form.</returns>
        public IHtmlContent Render(IHtmlHelper htmlHelper)
        {
            string pathBase = htmlHelper.ViewContext.HttpContext.Request.PathBase.Value ?? string.Empty;
            string actionUrl = $"{pathBase}/Search";
            string currentTerm = htmlHelper.ViewContext.HttpContext.Request.Query["term"].ToString();
            string label = WebLocalizer.GetInternal("NAVBAR_SEARCH_FORM_LABEL").Value;
            string placeholder = WebLocalizer.GetInternal("NAVBAR_SEARCH_FORM_PLACEHOLDER").Value;

            return new HtmlString($"""
                                   <form class="d-flex align-items-center dmb-search-navbar" method="get" action="{WebUtility.HtmlEncode(actionUrl)}" role="search">
                                       <label class="visually-hidden" for="dmb-search-navbar-term">{WebUtility.HtmlEncode(label)}</label>
                                       <input id="dmb-search-navbar-term" class="form-control form-control-sm" type="search" name="term" value="{WebUtility.HtmlEncode(currentTerm)}" placeholder="{WebUtility.HtmlEncode(placeholder)}" autocomplete="off" autocorrect="off" autocapitalize="none" spellcheck="false" />
                                   </form>
                                   """);
        }

        #endregion

        #endregion
    }
}