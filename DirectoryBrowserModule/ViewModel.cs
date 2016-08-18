using AssembliesTools.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Events;

namespace DirectoryBrowserModule
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _folderPath;
        public string FolderPath
        {
            get { return _folderPath; }
            set
            {
                _folderPath = value;
                NotifyPropertyChanged("FolderPath");
                _eventAggregator.GetEvent<FolderSelectedEvent>().Publish(new AssembliesTools.DomainModels.Folder(_folderPath));
            }
        }

        public ICommand BrowseFileCommand { get; private set; }
       
        private readonly IEventAggregator _eventAggregator;

        public ViewModel(IEventAggregator evetAggregator)
        {
            _eventAggregator = evetAggregator;
            BrowseFileCommand = new RelayCommand(o => BrowseFolder(o), o => true);
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

        private void BrowseFolder(object o)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = dialog.SelectedPath;
            }
        }
    }
}
