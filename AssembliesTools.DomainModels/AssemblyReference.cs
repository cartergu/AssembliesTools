using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace AssembliesTools.DomainModels
{
    public class AssemblyReference : INotifyPropertyChanged
    {
        private string _assembly;
        public string Assembly
        {
            get { return _assembly; }
            private set
            {
                _assembly = value;
                NotifyPropertyChanged();
            }
        }

        private string _referenceName;
        public string ReferenceName
        {
            get { return _referenceName; }
            private set
            {
                _referenceName = value;
                NotifyPropertyChanged();
            }
        }

        public string _referenceVersion;
        public string ReferenceVersion
        {
            get { return _referenceVersion; }
            private set
            {
                _referenceVersion = value;
                NotifyPropertyChanged();
            }
        }

        public AssemblyReference(string assembly, string referenceName, string referenceVersion)
        {
            Assembly = assembly;
            ReferenceName = referenceName;
            ReferenceVersion = referenceVersion;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
