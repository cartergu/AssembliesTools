using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfoProvider.Contracts
{
    public interface IAssemblyInfoProvider
    {
        AssemblyInfo GetAssemblyInfo(string assemblyPath);
    }
}
