using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfoProvider.Contracts
{
    public class AssemblyInfo
    {
        public string Name { get; private set; }

        public string FullName { get; private set; }

        public string Version { get; private set; }

        private readonly Lazy<IEnumerable<AssemblyReference>> _lazyReferenceCollection;

        public IEnumerable<AssemblyReference> ReferencedAssemblies {
            get
            {
                return _lazyReferenceCollection != null
                    ? _lazyReferenceCollection.Value
                    : Enumerable.Empty<AssemblyReference>();
            }
        }
        public AssemblyInfo(string name, string fullName, string version, Func<IEnumerable<AssemblyReference>> funcRefAssemblyInfos )
        {
            Name = name;
            FullName = fullName;
            Version = version;
            _lazyReferenceCollection = new Lazy<IEnumerable<AssemblyReference>>(funcRefAssemblyInfos); 
        }
    }

    public class AssemblyReference
    {
        public string ReferenceName { get; private set; }
        public string Version { get; private set; }

        public AssemblyReference(string refName, string refVersion)
        {
            ReferenceName = refName;
            Version = refVersion;
        }
    }
}
