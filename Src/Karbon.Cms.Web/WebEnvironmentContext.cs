using System.Web;
using Karbon.Cms.Core;

namespace Karbon.Cms.Web
{
    internal class WebEnvironmentContext : EnvironmentContext
    {
        private readonly HttpContextWrapper _httpContext;

        private string _rootDir;

        /// <summary>
        /// Gets the root directory.
        /// </summary>
        /// <value>
        /// The root directory.
        /// </value>
        public override string RootDirectory
        {
            get
            {
                return _rootDir ?? (_rootDir = MapPath("~/"));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebEnvironmentContext"/> class.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public WebEnvironmentContext(HttpContextWrapper httpContext)
        {
            _httpContext = httpContext;
        }

        /// <summary>
        /// Maps the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string MapPath(string path)
        {
            return _httpContext.Server.MapPath(path);
        }

        // <summary>
        // Gets the referenced assemblies.
        // </summary>
        // <returns></returns>
        //public override IEnumerable<Assembly> GetReferencedAssemblies()
        //{
        //    return _httpContext != null 
        //        ? BuildManager.GetReferencedAssemblies().Cast<Assembly>() 
        //        : base.GetReferencedAssemblies();
        //}
    }
}
