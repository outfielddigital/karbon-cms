using System.Web.Mvc;

namespace Karbon.Cms.Web.Filters
{
    /// <summary>
    /// Action filter to connect up the Karbon Text response filter.
    /// </summary>
    public class KarbonTextFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.Filter = new KarbonTextFilter(response.Filter);
        }
    }
}
