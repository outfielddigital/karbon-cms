using System.Configuration.Provider;

namespace Karbon.Cms.Core.Serialization
{
    internal class DataSerializerCollection : ProviderCollection
    {
        new public DataSerializer this[string name]
        {
            get { return (DataSerializer)base[name]; }
        }
    }
}
