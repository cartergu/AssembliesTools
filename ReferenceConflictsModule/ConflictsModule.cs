﻿

using System;
using AssembliesTools.Controls;
using Prism.Logging;
using Prism.Modularity;
using ModuleTracking;
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
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager _regionManager;


        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleA"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="moduleTracker">The module tracker.</param>        
        public ConflictsModule(IRegionManager regionManager, ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (moduleTracker == null)
            {
                throw new ArgumentNullException("moduleTracker");
            }

            this.logger = logger;
            this.moduleTracker = moduleTracker;
            this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleA);
            _regionManager = regionManager;

        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.logger.Log("ModuleA demonstrates logging during Initialize().", Category.Info, Priority.Medium);
            this.moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleA);
            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigationRegion, typeof(ConflictsNavigationItemView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, typeof(ReferenceConflictsView));
        }
    }
}