using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Core;
using Karbon.Cms.Web.Filters;
using Karbon.Cms.Web.Hosting;
using Karbon.Cms.Web.Routing;

namespace Karbon.Cms.Web
{
    public class WebBootManager : CoreBootManager
    {
        private bool _appInitedFlag;

        /// <summary>
        /// Initializes components that need to run after the application has started
        /// </summary>
        public override void Initialize()
        {
            if (_appInitedFlag)
                throw new InvalidOperationException("The boot manager has already been initialized");

            base.Initialize();

            // Wrap the http context
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);

            // Reset the environment context
            KarbonAppContext.Current.Environment = new WebEnvironmentContext(httpContextBase);

            // Register the media VPP
            HostingEnvironment.RegisterVirtualPathProvider(new MediaVirtualPathProvider());

            // Regitser global filters
            RegisterFilters();

            // Register required routes
            RegisterRoutes();

            _appInitedFlag = true;
        }

        /// <summary>
        /// Registers the filters.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        protected void RegisterFilters()
        {
            GlobalFilters.Filters.Add(new KarbonTextFilterAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        protected virtual void RegisterRoutes()
        {
            // Add the content route
            RouteTable.Routes.Insert(0, 
                new KarbonRoute(
                    "{*path}",
                    new RouteValueDictionary(new
                    {
                        controller = "Karbon", 
                        action = "Index"
                    }),
                    new MvcRouteHandler()));

            // Ignore media routes, these will be handled by the media VPP
            RouteTable.Routes.Ignore("media/{*path}");

            // Ignore axd routes (incase this isn't an MVC app by default)
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}
