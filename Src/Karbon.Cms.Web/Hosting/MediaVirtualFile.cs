using System.IO;
using System.Web.Hosting;
using Karbon.Cms.Core.IO;

namespace Karbon.Cms.Web.Hosting
{
    internal class MediaVirtualFile : VirtualFile
    {
        private FileStore _fileStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaVirtualFile"/> class.
        /// </summary>
        /// <param name="virtualPath">The virtual path to the resource represented by this instance.</param>
        public MediaVirtualFile(string virtualPath) 
            : base(virtualPath)
        {
            _fileStore = FileStoreManager.Default;
        }

        /// <summary>
        /// Returns a read-only stream to the virtual resource.
        /// </summary>
        /// <returns>
        /// A read-only stream to the virtual file.
        /// </returns>
        public override Stream Open()
        {
            return _fileStore.OpenFile(VirtualPath);
        }
    }
}
