using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ModuleTracking;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace DirectoryBrowserModule
{
    public class DirectoryModule : IModule
    {
        private readonly ILoggerFacade logger;
        private readonly IModuleTracker moduleTracker;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleA"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="moduleTracker">The module tracker.</param>        
        public DirectoryModule(IRegionManager regionManager, IUnityContainer container, ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            if (moduleTracker == null)
                throw new ArgumentNullException("moduleTracker");

            this.logger = logger;
            this.moduleTracker = moduleTracker;
            this._regionManager = regionManager;

            _container = container;

            this.moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleA);
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.logger.Log("ModuleA demonstrates logging during Initialize().", Category.Info, Priority.Medium);
            this.moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleA);
            _regionManager.RegisterViewWithRegion("TopRegion", typeof(SelectFolderControl));
        }
    }
}
