

using System;
using AssembliesTools.Controls;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Events;
using Prism.Regions;
using ReferenceConflictsModule.Views;

namespace ReferenceConflictsModule
{
    /// <summary>
    /// A module for the quickstart.
    /// </summary>    
    public class ConflictsModule : IModule
    {
        private readonly ILoggerFacade logger;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleA"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="moduleTracker">The module tracker.</param>        
        public ConflictsModule(IRegionManager regionManager, IUnityContainer container,  ILoggerFacade logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.logger = logger;
            _regionManager = regionManager;
            _container = container;
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
           
            this.logger.Log("ModuleA demonstrates logging during Initialize().", Category.Info, Priority.Medium);
            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ConflictsNavigationItemView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ReferenceConflictsView));
        }
    }
}
