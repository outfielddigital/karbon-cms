using System;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Core
{
    public class CoreBootManager
    {
        private bool _appInitedFlag;

        /// <summary>
        /// Initializes components that need to run after the application has started
        /// </summary>
        public virtual void Initialize()
        {
            if (_appInitedFlag)
                throw new InvalidOperationException("The boot manager has already been initialized");

            KarbonAppContext.Current = new KarbonAppContext();

            StoreManager.ContentStore = new ContentStore();

            _appInitedFlag = true;
        }
    }
}
