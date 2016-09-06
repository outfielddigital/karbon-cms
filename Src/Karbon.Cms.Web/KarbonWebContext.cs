using System;
using System.Web;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    public class KarbonWebContext
    {
        #region Factory

        internal KarbonWebContext()
        {
        }

        private const string HttpContextItemName = "Karbon.Cms.Web.KarbonWebContext";
        private static readonly object Locker = new object();

        private static KarbonWebContext _context;
        public static KarbonWebContext Current
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                    return (KarbonWebContext)System.Web.HttpContext.Current.Items[HttpContextItemName];
                
                return _context;
            }
            set 
            { 
                lock (Locker)
                {
                    // if running in a real HttpContext, this can only be set once
                    if (System.Web.HttpContext.Current != null && Current != null)
                        throw new ApplicationException("The current KarbonWebContext can only be set once during a request.");

                    // if there is an HttpContext, return the item
                    if (System.Web.HttpContext.Current != null)
                    {
                        System.Web.HttpContext.Current.Items.Add(HttpContextItemName, value);
                    }
                    else
                    {
                        _context = value;
                    }
                }
            }
        }

        #endregion

        public HttpContextBase HttpContext { get; private set; }

        public IContent CurrentPage { get; internal set; }
        public IContent HomePage { get; internal set; }

        internal KarbonWebContext(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            HttpContext = httpContext;
        }
    }
}
