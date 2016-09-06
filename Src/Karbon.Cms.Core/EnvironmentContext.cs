using System;
using System.IO;
using System.Reflection;

namespace Karbon.Cms.Core
{
    internal class EnvironmentContext
    {
        private string _rootDir;

        /// <summary>
        /// Gets the root directory.
        /// </summary>
        /// <value>
        /// The root directory.
        /// </value>
        public virtual string RootDirectory
        {
            get
            {
                if (!String.IsNullOrEmpty(_rootDir))
                {
                    return _rootDir;
                }

                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new Uri(codeBase);
                var path = uri.LocalPath;
                var baseDirectory = Path.GetDirectoryName(path);

                if (baseDirectory != null)
                    _rootDir = baseDirectory.Substring(0, baseDirectory.LastIndexOf("bin") - 1);

                return _rootDir;
            }
        }

        /// <summary>
        /// Maps the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public virtual string MapPath(string path)
        {
            var newPath = path.TrimStart('~', '/').Replace('/', Path.DirectorySeparatorChar);
            var mappedPath = RootDirectory + Path.DirectorySeparatorChar + newPath;
            return mappedPath;
        }

        // <summary>
        // Gets the referenced assemblies.
        // </summary>
        // <returns></returns>
        //public virtual IEnumerable<Assembly> GetReferencedAssemblies()
        //{
        //    return AppDomain.CurrentDomain.GetAssemblies().ToList();
        //}
    }
}
