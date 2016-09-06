using System.Linq;
using System.Web;
using System.Web.Hosting;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web.Hosting
{
    internal class MediaVirtualPathProvider : VirtualPathProvider
    {
        private IContentStore _contentStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaVirtualPathProvider"/> class.
        /// </summary>
        public MediaVirtualPathProvider()
        {
            _contentStore = StoreManager.ContentStore;
        }

        /// <summary>
        /// Gets a value that indicates whether a file exists in the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
        public override bool FileExists(string virtualPath)
        {
            return IsMediaPath(virtualPath) || base.FileExists(virtualPath);
        }

        /// <summary>
        /// Gets a virtual file from the virtual file system.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile" /> class that represents a file in the virtual file system.
        /// </returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            if(IsMediaPath(virtualPath))
            {
                var file = GetFileFromVirtualPath(virtualPath);
                return file != null 
                    ? new MediaVirtualFile(file.RelativePath)
                    : null;
            }
            
            return base.GetFile(virtualPath);
        }

        /// <summary>
        /// Determines whether the specified virtual path is a media path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>
        ///   <c>true</c> if the specified virtual path is a media path; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMediaPath(string virtualPath)
        {
            return VirtualPathUtility.ToAppRelative(virtualPath).StartsWith("~/media/");
        }

        /// <summary>
        /// Gets the file from virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        private IFile GetFileFromVirtualPath(string virtualPath)
        {
            // Trim the media prefix
            var fileRelativeUrl = virtualPath.Substring(virtualPath.LastIndexOf("/media/") + 7);

            // Workout content url + file slug
            var fileSlug = fileRelativeUrl;
            var contentRelativeUrl = "";

            if (fileRelativeUrl.LastIndexOf('/') > -1)
            {
                fileSlug = fileRelativeUrl.Substring(fileRelativeUrl.LastIndexOf('/') + 1);
                contentRelativeUrl = fileRelativeUrl.Substring(0, fileRelativeUrl.LastIndexOf('/'));
            }

            // Find the content
            var content = _contentStore.GetByUrl("~/" + contentRelativeUrl);
            if (content == null)
                return null;

            // Find the file
            var file = content.AllFiles.SingleOrDefault(x => x.Slug.ToLowerInvariant() == fileSlug.ToLowerInvariant());
            if (file == null)
                return null;

            return file;
        }
    }
}
