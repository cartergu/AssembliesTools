using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssembliesTools.Controls;
using AssembliesTools.ListingModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace AssembliesTools.ListingModule
{
    public class ListingModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ListingModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ListingNavigationItemView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ListingView));
        }
    }
}
