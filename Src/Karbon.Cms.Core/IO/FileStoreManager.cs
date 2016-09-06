using System;
using System.Configuration;
using System.Web.Configuration;
using Karbon.Cms.Core.Configuration;

namespace Karbon.Cms.Core.IO
{
    internal class FileStoreManager
    {
        private static FileStore _defaultProvider;
        private static FileStoreCollection _providers;

        /// <summary>
        /// Initializes the <see cref="FileStoreManager"/> class.
        /// </summary>
        static FileStoreManager()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">fileStores configuration section is not set correctly.</exception>
        /// <exception cref="System.Exception">_defaultProvider</exception>
        private static void Initialize()
        {
            // Parse config
            var config = (FileStoresSection)ConfigurationManager.GetSection("karbon/fileStores");
            if (config == null)
                throw new ConfigurationErrorsException("fileStores configuration section is not set correctly.");

            // Create providers
            _providers = new FileStoreCollection();

            ProvidersHelper.InstantiateProviders(config.Providers, 
                _providers, typeof(FileStore));

            _providers.SetReadOnly();

            // Set default provider
            _defaultProvider = _providers[config.Default];

            if (_defaultProvider == null)
                throw new Exception("_defaultProvider");
        }

        /// <summary>
        /// Gets the file stores.
        /// </summary>
        /// <value>
        /// The file stores.
        /// </value>
        public static FileStoreCollection Providers
        {
            get { return _providers; }
        }

        /// <summary>
        /// Gets the default file store.
        /// </summary>
        /// <value>
        /// The default file store.
        /// </value>
        public static FileStore Default
        {
            get { return _defaultProvider; }
        }
    }
}
