using System.Web;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    public static class EntityApiExtensions
    {
        /// <summary>
        /// Gets the absolute url for the given entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static string Url(this IEntity entity)
        {
            return VirtualPathUtility.ToAbsolute(entity.RelativeUrl);
        }
    }
}
