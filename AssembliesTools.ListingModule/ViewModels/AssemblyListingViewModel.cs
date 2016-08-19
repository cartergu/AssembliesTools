using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssembliesTools.DomainModels;
using AssemblyInfoProvider.Contracts;
using Prism.Events;
using AssemblyReference = AssembliesTools.DomainModels.AssemblyReference;

namespace AssembliesTools.ListingModule.ViewModels
{
    public class AssemblyListingViewModel : INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IAssemblyInfoProvider _assemblyInfoProvider;

        public AssemblyListingViewModel(IEventAggregator evetAggregator, IAssemblyInfoProvider assemblyInfoProvider)
        {
            _eventAggregator = evetAggregator;
            _assemblyInfoProvider = assemblyInfoProvider;
            _eventAggregator.GetEvent<FolderSelectedEvent>().Subscribe(Refresh);
        }

        private ObservableCollection<AssemblyViewModel> _assemblyViewModels;

        public ObservableCollection<AssemblyViewModel> AssemblyViewModels
        {
            get { return _assemblyViewModels; }
            set
            {
                _assemblyViewModels = value;
                NotifyPropertyChanged("AssemblyViewModels");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Refresh(Folder folder)
        {
            var diretoryInfo = new DirectoryInfo(folder.FullPath);
            _assemblyViewModels = new ObservableCollection<AssemblyViewModel>(); //.Clear();

            foreach (var o in diretoryInfo.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly).Concat(diretoryInfo.EnumerateFiles("*.exe", SearchOption.TopDirectoryOnly)))
            {
                var asmInfo = _assemblyInfoProvider.GetAssemblyInfo(o.FullName);

                if(asmInfo != null) 
                    _assemblyViewModels.Add(new AssemblyViewModel(asmInfo.Name, asmInfo.FullName, asmInfo.Version, asmInfo.ReferencedAssemblies
                        .Select(t=> new AssemblyReference(asmInfo.Name, t.ReferenceName, t.Version)).ToArray()));
            }

            NotifyPropertyChanged("AssemblyViewModels");
        }
    }
}
