using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AssemblyInfoProvider.Contracts;
using Microsoft.Practices.ServiceLocation;
using ReferenceConflictsModule.Native;
using AssemblyReference = AssembliesTools.DomainModels.AssemblyReference;

namespace ReferenceConflictsModule
{
    public class DependecyAnalayzer
    {
        private readonly IAssemblyInfoProvider _assemblyInfoProvider;
        private DependecyAnalayzer(IAssemblyInfoProvider assemblyInfoProvider)
        {
            _assemblyInfoProvider = assemblyInfoProvider;
        }

        private static Lazy<DependecyAnalayzer> dependencyAnalayzerLazy = new Lazy<DependecyAnalayzer>
             (() => new DependecyAnalayzer(ServiceLocator.Current.GetInstance<IAssemblyInfoProvider>()));

        public static DependecyAnalayzer Instance
        {
            get { return dependencyAnalayzerLazy.Value; }
        }

        public IEnumerable<AssemblyReference> GetDependeciesReferences(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            var assemblyFiles = directoryInfo.GetFiles("*.dll").Concat(directoryInfo.GetFiles("*.exe"));

            return GetAssemblyReferences(assemblyFiles);
        }

        public IEnumerable<AssemblyReference> GetDependeciesReferencesConflicts(IEnumerable<AssemblyReference> assemblyReferences, bool skipSystem)
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

        public IEnumerable<AssemblyReference> GetAssemblyReferences(IEnumerable<FileInfo> fileInfos)
        {
            foreach (var fileInfo in fileInfos.OrderBy(asm => asm.Name))
            {
                var assemblyInfo = _assemblyInfoProvider.GetAssemblyInfo(fileInfo.FullName);
                if(assemblyInfo != null)
                    foreach (var referencedAssembly in assemblyInfo.ReferencedAssemblies)
                        yield return new AssemblyReference(assemblyInfo.Name, referencedAssembly.ReferenceName, referencedAssembly.Version);
            }
        }
    }
}
