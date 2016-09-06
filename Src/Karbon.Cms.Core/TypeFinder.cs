using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Karbon.Cms.Core.Threading;

namespace Karbon.Cms.Core
{
    internal class TypeFinder
    {
        private static readonly string[] ExcludedAssemblies = new[]
        {
            "System.",
            "Microsoft.",
            "Newtonsoft.",
            "Karbon."
        };

        private static ReadOnlyCollection<Assembly> _assemblies = null;
        private static IDictionary<string, IEnumerable<Type>> _typeMap = new ConcurrentDictionary<string, IEnumerable<Type>>();

        private static readonly ReaderWriterLockSlim _assembliesLock = new ReaderWriterLockSlim();
        private static readonly ReaderWriterLockSlim _typeMapLock = new ReaderWriterLockSlim();

        private static IEnumerable<Assembly> GetAssemblies()
        {
            if (_assemblies == null)
            {
                using (new WriteLock(_assembliesLock))
                {
                    if (_assemblies == null)
                    {
                        var binFolder = Assembly.GetExecutingAssembly().GetAssemblyFileInfo().Directory;
                        if (binFolder != null)
                        {
                            var assemblyFiles = Directory.GetFiles(binFolder.FullName, "*.dll",
                                                                   SearchOption.TopDirectoryOnly);
                            var assemblies = assemblyFiles
                                .Where(x => ExcludedAssemblies.All(y => !x.StartsWith(y)))
                                .Select(Assembly.LoadFrom)
                                .ToList();

                            _assemblies = new ReadOnlyCollection<Assembly>(assemblies);
                        }
                    }
                }
            }

            return _assemblies;
        }

        public static IEnumerable<Type> FindTypes<TType>(bool concreteOnly = true)
        {
            var tType = typeof(TType);
            var mapKey = string.Format("TypeMap_{0}_{1}", tType.FullName, concreteOnly);

            if (!_typeMap.ContainsKey(mapKey))
            {
                using (new WriteLock(_typeMapLock))
                {
                    if (!_typeMap.ContainsKey(mapKey))
                    {
                        var types = GetAssemblies()
                            .SelectMany(a => a.GetExportedTypes())
                            .Where(t => !t.IsInterface && tType.IsAssignableFrom(t)
                                        && (!concreteOnly || (t.IsClass && !t.IsAbstract)));

                        _typeMap.Add(mapKey, types);
                    }
                }
            }

            return _typeMap[mapKey];
        }
    }
}
