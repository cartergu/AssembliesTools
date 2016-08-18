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
using AssembliesTools.ListingModule.ViewModels;

namespace AssembliesTools.ListingModule.Views
{
    /// <summary>
    /// Interaction logic for ListingView.xaml
    /// </summary>
    [Export("ListingView")]    
    public partial class ListingView : UserControl
    {
        public ListingView()
        {
            InitializeComponent();
        }

        public ListingView(AssemblyListingViewModel viewModel)
            : this()
        {
            this.DataContext = viewModel;
        }
    }
}
