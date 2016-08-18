using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;

namespace DirectoryBrowserModule
{
    public class DirectoryModule : IModule
    {
        private readonly ILoggerFacade logger;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleA"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="moduleTracker">The module tracker.</param>        
        public DirectoryModule(IRegionManager regionManager, IUnityContainer container, ILoggerFacade logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;
            this._regionManager = regionManager;
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this.logger.Log("ModuleA demonstrates logging during Initialize().", Category.Info, Priority.Medium);
            _regionManager.RegisterViewWithRegion("TopRegion", typeof(SelectFolderControl));
        }
    }
}
