using System.Web;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    /// <summary>
    /// Contains API content methods that are dependant on the web context
    /// </summary>
    public static class ContentApiExtensions
    {
        /// <summary>
        /// Determines whether the specified content is open.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is open; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOpen(this IContent content)
        {
            var contentUrl = content.Url();
            var currentUrl = HttpContext.Current.Request.Url.LocalPath;
            return currentUrl == contentUrl || currentUrl.StartsWith(contentUrl + "/");
            // NOTE: This currently reports homepage as closed if a sub page is open
            // need to decide if this is the desired effect, or whether homepage should be open too
        }
    }
}
