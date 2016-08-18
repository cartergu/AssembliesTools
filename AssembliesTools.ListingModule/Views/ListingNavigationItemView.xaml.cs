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
using AssembliesTools.Controls;
using Prism.Regions;

namespace AssembliesTools.ListingModule.Views
{
    /// <summary>
    /// Interaction logic for ListingNavigationItemView.xaml
    /// </summary>
    public partial class ListingNavigationItemView : UserControl, IPartImportsSatisfiedNotification
    {
        private static readonly Uri viewUrl = new Uri("ListingView", UriKind.Relative);

        private readonly IRegionManager _regionManager;

        public ListingNavigationItemView(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent();
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            IRegion mainContentRegion = this._regionManager.Regions[RegionNames.MainContentRegion];
            if (mainContentRegion != null && mainContentRegion.NavigationService != null)
            {
                mainContentRegion.NavigationService.Navigated += this.MainContentRegion_Navigated;
            }
        }

        public void MainContentRegion_Navigated(object sender, RegionNavigationEventArgs e)
        {
            this.UpdateNavigationButtonState(e.Uri);
        }

        private void UpdateNavigationButtonState(Uri uri)
        {
            this.btnNavigateToAssembliesList.IsChecked = (uri == viewUrl);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, viewUrl);
        }
    }
}


