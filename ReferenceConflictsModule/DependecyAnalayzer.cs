using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AssembliesTools.DomainModels;
using ReferenceConflictsModule.Native;

namespace ReferenceConflictsModule
{
    public static class DependecyAnalayzer
    {
        public static IEnumerable<AssemblyReference> GetDependeciesReferences(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            var assemblyFiles = directoryInfo.GetFiles("*.dll").Concat(directoryInfo.GetFiles("*.exe"));

            return GetAssemblyReferences(assemblyFiles);
        }

        public static IEnumerable<AssemblyReference> GetDependeciesReferencesConflicts(IEnumerable<AssemblyReference> assemblyReferences, bool skipSystem)
        {
            foreach (var gr in assemblyReferences.GroupBy(o => o.ReferenceName))
            {
                if (gr.Key == null || 
                    skipSystem && (gr.Key.StartsWith("System") || gr.Key.StartsWith("mscorlib"))) continue;

                if (gr.Select(o => o.ReferenceVersion).Distinct().Count() > 1) // if have more than one version exist
                {
                    foreach (var o in gr.OrderBy(o => o.ReferenceVersion))
                        yield return o;
                }
            }
        }

        public static IEnumerable<AssemblyReference> GetAssemblyReferences(IEnumerable<FileInfo> fileInfos)
        {
            foreach (var fileInfo in fileInfos.OrderBy(asm => asm.Name))
            {
                Assembly assembly = null;
                try
                {
                    if (!fileInfo.IsAssembly()) continue;
                   
                    assembly = Assembly.ReflectionOnlyLoadFrom(fileInfo.FullName);
                }
                catch (Exception ex)
                {
                    //_failedToLoadAssemblies.Add(fileInfo.Name);
                    continue;
                }

                foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
                    yield return new AssemblyReference(assembly.GetName().Name, referencedAssembly.Name, referencedAssembly.Version);
                
            }
        }
    }
}
