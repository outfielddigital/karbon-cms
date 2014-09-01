using System;
using System.Linq;
using System.Web.Routing;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web.Routing
{
    public class KarbonRoute : Route
    {
        private readonly RouteValueDictionary _defaults;
        private readonly IRouteHandler _routeHandler;

        /// <summary>
        /// Gets the controller key.
        /// </summary>
        /// <value>
        /// The controller key.
        /// </value>
        public static string ControllerKey 
        {
            get { return "controller"; }
        }

        /// <summary>
        /// Gets the action key.
        /// </summary>
        /// <value>
        /// The action key.
        /// </value>
        public static string ActionKey
        {
            get { return "action"; }
        }

        /// <summary>
        /// Gets the default controller.
        /// </summary>
        /// <value>
        /// The default controller.
        /// </value>
        public static string DefaultController
        {
            get { return "KarbonController"; }
        }

        /// <summary>
        /// Gets the default action.
        /// </summary>
        /// <value>
        /// The default action.
        /// </value>
        public static string DefaultAction
        {
            get { return "Index"; }
        }


        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            // Setup the route
            var routeData = new RouteData(this, _routeHandler);
            var virtualPath = httpContext.Request.CurrentExecutionFilePath
                .TrimStart(new[] { '/' });

            var action = DefaultAction;

            // Try and grab the model for the given URL
            var model = StoreManager.ContentStore.GetByUrl("~/" + virtualPath);
            if (model == null)
            {
                // Try to load the page without the last segment of the url and set the last segment as action
                if (virtualPath.LastIndexOf("/", StringComparison.InvariantCulture) > 0)
                {
                    var index = virtualPath.LastIndexOf("/", StringComparison.InvariantCulture);

                    var tmpAction = virtualPath.Substring(index, virtualPath.Length - index).Trim(new[] {'/'});
                    var tmpVirtualPath = virtualPath.Substring(0, index).TrimStart(new[] {'/'});

                    model = StoreManager.ContentStore.GetByUrl("~/" + tmpVirtualPath);
                    if (model != null)
                    {
                        virtualPath = tmpVirtualPath;
                        action = tmpAction;
                    }
                }
                else 
                {
                    model = StoreManager.ContentStore.GetByUrl("~/");
                    if (model != null)
                    {
                        action = virtualPath;
                    }
                }
            }

            // If by now we haven't found a model, let MVC see if there
            // is a better route registered
            if (model == null)
                return null;

            // If it's the homepage and we need to ignore the homepage then passthrough
            if (KarbonAppContext.Current.IgnoreHomepage && model.IsHomePage())
                return null;

            // We have a model, so lets work out where to direct the request
            var controller = model.GetController(_defaults != null && _defaults.ContainsKey(ControllerKey) ? _defaults[ControllerKey] + "Controller" : DefaultController);
            if (controller == null || !controller.HasMethod(action))
                return null; 

            // Route the request
            routeData.Values[ControllerKey] = controller.Name.TrimEnd("Controller");
            routeData.Values[ActionKey] = action;
            
            // Set the current page on current web context
            KarbonWebContext.Current.CurrentPage = model;

            return routeData;
        }

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        /// <returns>
        /// An object that contains information about the URL that is associated with the route.
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (KarbonWebContext.Current == null)
                return null;

            // Check if this route should be handled by Karbon. If not, fallback to other handlers (MVC)
            var routeRoot = (string) (values["area"] ?? values["controller"]);
            if (routeRoot != null)
                if (!KarbonWebContext.Current.HomePage.Children().Any(x => x.Slug.Equals(routeRoot, StringComparison.InvariantCultureIgnoreCase)))
                    return null;

            // Grab the model from the route data collection
            var model = KarbonWebContext.Current.CurrentPage;
            if (model == null)
                return null;

            // Create virtual path from model url
            var vpd = new VirtualPathData(this, model.RelativeUrl.TrimStart("~/"));

            // Append any other route data values as querystring params
            var queryParams = values.Where(kvp => !kvp.Key.Equals(ControllerKey) 
                && !kvp.Key.Equals(ActionKey))
                .ToQueryString();
            
            vpd.VirtualPath += queryParams;

            // Return the virtual path
            return vpd;
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonRoute"/> class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public KarbonRoute(string url, 
            IRouteHandler routeHandler) 
            : base(url, routeHandler)
        {
            _routeHandler = routeHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonRoute"/> class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public KarbonRoute(string url, 
            RouteValueDictionary defaults, 
            IRouteHandler routeHandler) 
            : base(url, defaults, routeHandler)
        {
            _defaults = defaults;
            _routeHandler = routeHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonRoute"/> class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public KarbonRoute(string url, 
            RouteValueDictionary defaults, 
            RouteValueDictionary constraints, 
            IRouteHandler routeHandler) 
            : base(url, defaults, constraints, routeHandler)
        {
            _defaults = defaults;
            _routeHandler = routeHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonRoute"/> class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public KarbonRoute(string url, 
            RouteValueDictionary defaults, 
            RouteValueDictionary constraints, 
            RouteValueDictionary dataTokens, 
            IRouteHandler routeHandler) 
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _defaults = defaults;
            _routeHandler = routeHandler;
        }

        #endregion
    }
}
