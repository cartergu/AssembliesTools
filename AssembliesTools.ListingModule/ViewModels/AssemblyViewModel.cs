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

        public AssemblyViewModel(FileInfo fi)
        {
            Name = fi.Name;
            FullName = fi.FullName;
            Version = FileVersionInfo.GetVersionInfo(fi.FullName).FileVersion;
            try
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(o => o.Location == fi.FullName);
                
                if(assembly == null)
                    assembly = Assembly.ReflectionOnlyLoadFrom(fi.FullName);

                ReferencedAssemblies =
                    assembly.GetReferencedAssemblies()
                        .Select(o => new AssemblyReference(fi.Name, o.Name, o.Version))
                        .ToArray();

                DotNetVersion = GetDotNetVersion();
            }
            catch (Exception)
            {
            }

            //Version = AssemblyName.GetAssemblyName(fi.FullName).Version.ToString();
            //Version = fi.
        }

        private string GetDotNetVersion()
        {
            var dotNetReference = ReferencedAssemblies.FirstOrDefault(o => o.ReferenceName == "mscorlib");
            return dotNetReference != null ? dotNetReference.ReferenceVersion.ToString() : null;
        }
    }
}
