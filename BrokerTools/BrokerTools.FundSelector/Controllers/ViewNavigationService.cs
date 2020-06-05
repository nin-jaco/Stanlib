using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrokerTools.FundSelector.Views;
using System.ComponentModel;

namespace BrokerTools.FundSelector.Controllers
{
    public sealed class ViewNavigationService
    {
        #region :: Fields ::

        private MainView _MainView;

        /// <summary>
        /// Field backing the "Instance" property.
        /// </summary>
        static volatile ViewNavigationService _Instance;

        /// <summary>
        /// Thread lock.
        /// </summary>
        static object _SyncRoot = new Object();

        #endregion

        #region :: Properties ::

        public ViewNavigationController ViewNavigator { get; private set; }

        public UserControl MainView
        {
            get { return _MainView; }
        }

        public UserControl MainViewContentHost
        {
            get { return _MainView.MainViewContent.Content as UserControl; }
            set { _MainView.MainViewContent.Content = value; }
        }

        /// <summary>
        /// Gets the viewmodel container instance.
        /// </summary>
        public static ViewNavigationService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new ViewNavigationService();
                        }
                    }
                }
                return _Instance;
            }
        }

        #endregion

        #region :: Constructor ::

        ViewNavigationService()
        {
            _MainView = new MainView();
            ViewNavigator = new ViewNavigationController(view => MainViewContentHost = view);
        }
        #endregion

    }
}
