#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.IO;
using Microsoft.AspNetCore.Hosting;

#endregion

namespace DMBSearchViewer
{
    /// <summary>
    ///     Resolves DMBSearchViewer database paths against the host content root.
    /// </summary>
    public static class DMBSearchPathResolver
    {
        #region Static methods

        /// <summary>
        ///     Resolves an absolute path or a content-root relative path.
        /// </summary>
        /// <param name="environment">The web host environment that supplies the content root.</param>
        /// <param name="databasePath">The configured database path.</param>
        /// <returns>The absolute database path.</returns>
        public static string Resolve(IWebHostEnvironment environment, string databasePath)
        {
            if (Path.IsPathRooted(databasePath))
            {
                return databasePath;
            }

            return Path.GetFullPath(Path.Combine(environment.ContentRootPath, databasePath));
        }

        #endregion
    }
}