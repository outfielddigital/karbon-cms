using System;
using System.Configuration;
using System.Web.Configuration;
using Karbon.Cms.Core.Configuration;

namespace Karbon.Cms.Core.Serialization
{
    internal class DataSerializerManager
    {
        private static DataSerializer _defaultProvider;
        private static DataSerializerCollection _providers;

        /// <summary>
        /// Initializes the <see cref="DataSerializerManager"/> class.
        /// </summary>
        static DataSerializerManager()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">dataSerializers configuration section is not set correctly.</exception>
        /// <exception cref="System.Exception">_defaultProvider</exception>
        private static void Initialize()
        {
            // Parse config
            var config = (DataSerializersSection)ConfigurationManager.GetSection("karbon/dataSerializers");
            if (config == null)
                throw new ConfigurationErrorsException("dataSerializers configuration section is not set correctly.");

            // Create providers
            _providers = new DataSerializerCollection();

            ProvidersHelper.InstantiateProviders(config.Providers,
                _providers, typeof(DataSerializer));

            _providers.SetReadOnly();

            // Get default provider
            _defaultProvider = _providers[config.Default];

            if (_defaultProvider == null)
                throw new Exception("_defaultProvider");
        }

        /// <summary>
        /// Gets the serializers.
        /// </summary>
        /// <value>
        /// The serializers.
        /// </value>
        public static DataSerializerCollection Providers
        {
            get { return _providers; }
        }

        /// <summary>
        /// Gets the default serializer.
        /// </summary>
        /// <value>
        /// The default serializer.
        /// </value>
        public static DataSerializer Default
        {
            get { return _defaultProvider; }
        }
    }
}
