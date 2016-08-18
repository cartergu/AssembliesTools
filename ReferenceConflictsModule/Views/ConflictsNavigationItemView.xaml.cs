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

namespace ReferenceConflictsModule.Views
{
    /// <summary>
    /// Interaction logic for ConflictsNavigationItemView.xaml
    /// </summary>
    [Export]
    [ViewSortHint("01")]
    public partial class ConflictsNavigationItemView : UserControl, IPartImportsSatisfiedNotification
    {
        private static readonly Uri conflictViewUri = new Uri("ReferenceConflictsView", UriKind.Relative);

        private readonly IRegionManager _regionManager;

        public ConflictsNavigationItemView(IRegionManager regionManager)
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
            this.btnNavigateToConflictView.IsChecked = (uri == conflictViewUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, conflictViewUri);
        }
    }
}
