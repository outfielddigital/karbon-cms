using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;

namespace Karbon.Cms.Core.Serialization
{
    internal abstract class DataSerializer : ProviderBase
    {
        /// <summary>
        /// Gets the supported file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public string FileExtension { get; private set; }

        /// <summary>
        /// Initializes the serializer.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name,
            NameValueCollection config)
        {
            base.Initialize(name, config);

            FileExtension = config["fileExtension"];

            Initialize(config);
        }

        /// <summary>
        /// Initializes the serializer.
        /// </summary>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public virtual void Initialize(NameValueCollection config) { }

        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public abstract IDictionary<string, string> Deserialize(Stream data);
    }
}
