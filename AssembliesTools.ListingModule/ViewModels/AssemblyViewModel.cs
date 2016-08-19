using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssembliesTools.DomainModels;

namespace AssembliesTools.ListingModule.ViewModels
{
    public class AssemblyViewModel
    {
        public string Name { get; private set; }

        public string FullName { get; private set; }

        public string Version { get; private set; }

        public string DotNetVersion { get; private set; }

        public AssemblyReference[] ReferencedAssemblies { get; private set; }

        public AssemblyReference[] AssembliesReferencedBy { get; private set; }

        public string[] ExportedTypes { get; private set; }

        public AssemblyViewModel(string name, string fullName, string version, AssemblyReference[] assemblyReferences)
        {
            this.Name = name;
            this.FullName = fullName;
            this.Version = version;
            this.ReferencedAssemblies = assemblyReferences;
            DotNetVersion = GetDotNetVersion();
        }
 
        private string GetDotNetVersion()
        {
            var dotNetReference = ReferencedAssemblies.FirstOrDefault(o => o.ReferenceName == "mscorlib");
            return dotNetReference?.ReferenceVersion;
        }
    }
}
