using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using ReferenceConflictsModule.ViewModels;

namespace ReferenceConflictsModule.Views
{
    /// <summary>
    /// Interaction logic for ReferenceConflictsView.xaml
    /// </summary>
    [Export("ReferenceConflictsView")]
    public partial class ReferenceConflictsView 
    {
        public ReferenceConflictsView()
        {
            InitializeComponent();
        }

        public ReferenceConflictsView(ConflictsViewModel model) : this()
        {
            this.DataContext = model;
        }

        private void ReferenceConflictsView_OnLoaded(object sender, RoutedEventArgs e)
        {
        //    if(this.DataContext == null) this.DataContext = new ConflictsViewModel(null);
        }
    }
}
