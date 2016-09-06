using System.Configuration.Provider;

namespace Karbon.Cms.Core.IO
{
    internal class FileStoreCollection : ProviderCollection
    {
        new public FileStore this[string name]
        {
            get { return (FileStore)base[name]; }
        }
    }
}
