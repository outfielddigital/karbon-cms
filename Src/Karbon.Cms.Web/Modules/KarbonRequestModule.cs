﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web.Modules
{
    internal class KarbonRequestModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) =>
            {
                // Sync the content cache
                // We do it here so that we can ensure it will be fresh
                // at the begining of each request. If content changes
                // have occured, they will be re-synced, then the rest of 
                // the application can rest assured the cache is up to date.
                StoreManager.ContentStore.SyncCache();

                // If you came hear looking to see how Karbon requests are routed
                // you are in the wrong place, this module is purely for content
                // cache updating. Go take a look at the KarbonRoute class instead.
            };
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        { }
    }
}
