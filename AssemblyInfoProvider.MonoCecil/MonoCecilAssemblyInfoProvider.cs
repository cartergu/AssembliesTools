using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyInfoProvider.Contracts;
using Mono.Cecil;

namespace AssemblyInfoProvider.MonoCecil
{
    public class MonoCecilAssemblyInfoProvider : IAssemblyInfoProvider
    {
        public AssemblyInfo GetAssemblyInfo(string assemblyPath)
        {
            try
            {
                var asmDef = Mono.Cecil.AssemblyDefinition.ReadAssembly(assemblyPath);
                return new AssemblyInfo(asmDef.Name.Name, asmDef.FullName, asmDef.Name.Version.ToString(),
                    () => asmDef.MainModule.AssemblyReferences.Select(o => new AssemblyReference(o.Name, o.Version.ToString())).AsEnumerable());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
