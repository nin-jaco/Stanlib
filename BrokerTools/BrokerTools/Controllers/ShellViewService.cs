using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using BrokerTools.Views;
using BrokerTools.Data;

namespace BrokerTools.Controllers
{
    /// <summary>
    ///     Sterling application service
    /// </summary>
    public sealed class ShellViewService : IApplicationService, IApplicationLifetimeAware
    {
        private MainPage _mainPage;

        public UserControl MainVisual
        {
            get { return _mainPage.MainContent.Content as UserControl; }
            set { _mainPage.MainContent.Content = value; }
        }

        public static ShellViewService Current { get; private set; }

        /// <summary>
        ///     Navigator
        /// </summary>
        public Navigation Navigator { get; private set; }

        /// <summary>
        /// Called by an application in order to initialize the application extension service.
        /// </summary>
        /// <param name="context">Provides information about the application state. </param>
        public void StartService(ApplicationServiceContext context)
        {
            if (DesignerProperties.IsInDesignTool) return;

            Current = this;
        }

        /// <summary>
        /// Called by an application in order to stop the application extension service. 
        /// </summary>
        public void StopService()
        {
            return;
        }

        /// <summary>
        /// Called by an application immediately before the <see cref="E:System.Windows.Application.Startup"/> event occurs.
        /// </summary>
        public void Starting()
        {
            SiaqodbFactory.SetDBName("BrokerTools");

            _mainPage = new MainPage();
            Application.Current.RootVisual = _mainPage;
            Navigator = new Navigation(view => MainVisual = view);
        }

        /// <summary>
        /// Called by an application immediately after the <see cref="E:System.Windows.Application.Startup"/> event occurs.
        /// </summary>
        public void Started()
        {
            return;
        }

        /// <summary>
        /// Called by an application immediately before the <see cref="E:System.Windows.Application.Exit"/> event occurs. 
        /// </summary>
        public void Exiting()
        {
            if (DesignerProperties.IsInDesignTool) return;
        }

        /// <summary>
        /// Called by an application immediately after the <see cref="E:System.Windows.Application.Exit"/> event occurs. 
        /// </summary>
        public void Exited()
        {
            if (DesignerProperties.IsInDesignTool) return;
        }

        public void RequestRebuild()
        {
            if (RebuildRequested != null)
            {
                RebuildRequested(this, EventArgs.Empty);
            }
        }

        public event EventHandler RebuildRequested;

    }
}
