using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AssembliesTools.DomainModels;
using Prism.Events;

namespace ReferenceConflictsModule.ViewModels
{
    public class ConflictsViewModel : INotifyPropertyChanged
    {
        private ListCollectionView _collectionView = null;
        public ListCollectionView ReferenceDifferences
        {
            get { return _collectionView; }
            private set
            {
                _collectionView = value;
                NotifyPropertyChanged("ReferenceDifferences");
            }
        }

        private string _folderPath;
        public string AssembliesFolder
        {
            get { return _folderPath; }
            set
            {
                _folderPath = value;
                NotifyPropertyChanged("AssembliesFolder");
            }
        }

        public ICommand AnalyzeCommand { get; private set; }

        private readonly IEventAggregator _eventAggregator;

        public ConflictsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            AnalyzeCommand = new RelayCommand(o => Analyze(o), o => true);
            _eventAggregator.GetEvent<FolderSelectedEvent>().Subscribe(Refresh);
        }

        //public ConflictsViewModel(string folder) : this()
        //{
        //    _folderPath = folder;
        //    Analyze(null);
        //}

        private void Refresh(Folder folder)
        {
            if (_folderPath != folder.FullPath)
            {
                _folderPath = folder.FullPath;
                Analyze(null);
            }
        }

        private void Analyze(object o)
        {
            ReferenceDifferences = null;

            if (!string.IsNullOrEmpty(_folderPath) && System.IO.Directory.Exists(_folderPath))
            {
                var assemblyReferences = DependecyAnalayzer.Instance.GetDependeciesReferences(_folderPath);

                var referenceConflicts = DependecyAnalayzer.Instance.GetDependeciesReferencesConflicts(assemblyReferences, true).ToList();

                ReferenceDifferences = new ListCollectionView(referenceConflicts) 
                { 
                    GroupDescriptions = {
                    new PropertyGroupDescription("ReferenceName"),
                    new PropertyGroupDescription("ReferenceVersion")
                }};
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

    }
}
