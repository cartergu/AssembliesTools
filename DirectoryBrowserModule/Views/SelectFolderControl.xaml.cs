﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirectoryBrowserModule
{
    /// <summary>
    /// Interaction logic for SelectFolderControl.xaml
    /// </summary>
    public partial class SelectFolderControl : UserControl
    {
        public SelectFolderControl()
        {
            InitializeComponent();
        }

        public SelectFolderControl(ViewModel viewModel) : this()
        {
            this.DataContext = viewModel;
        }

        private void SelectFolderControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = new ViewModel();
        }
    }
}
