using System.Configuration;

namespace Karbon.Cms.Core.Configuration
{
    internal class DataSerializersSection : ConfigurationSection
    {
        [ConfigurationProperty("providers", IsDefaultCollection = true, IsRequired = true)]
        public ProviderSettingsCollection Providers
        {
            get { return ((ProviderSettingsCollection)base["providers"]); }
        }

        [ConfigurationProperty("default", DefaultValue = "karbon")]
        public string Default
        {
            get { return ((string)base["default"]); }
            set { base["default"] = value; }
        }
    }
}
