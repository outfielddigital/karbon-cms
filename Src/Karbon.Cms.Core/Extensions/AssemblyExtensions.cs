using System;
using System.IO;
using System.Reflection;

namespace Karbon.Cms.Core
{
    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Returns the file info of the assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static FileInfo GetAssemblyFileInfo(this Assembly assembly)
        {
            var codeBase = assembly.CodeBase;
            var uri = new Uri(codeBase);
            var path = uri.LocalPath;
            return new FileInfo(path);
        }

        /// <summary>
        ///  Returns the file info of the assembly
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static FileInfo GetAssemblyFileInfo(this AssemblyName assemblyName)
        {
            var codeBase = assemblyName.CodeBase;
            var uri = new Uri(codeBase);
            var path = uri.LocalPath;
            return new FileInfo(path);
        }
    }
}
