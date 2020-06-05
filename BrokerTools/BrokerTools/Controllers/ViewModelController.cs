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
using BrokerTools.ViewModels;

namespace BrokerTools.Controllers
{
    /// <summary>
    /// Class ViewModelContainer
    /// </summary>
    public sealed class ViewModelContainer
    {
        #region :: Fields ::

        /// <summary>
        /// Field backing the "Instance" property.
        /// </summary>
        static volatile ViewModelContainer _Instance;

        /// <summary>
        /// Thread lock.
        /// </summary>
        static object _SyncRoot = new Object();

        #endregion

        #region :: Properties ::

        /// <summary>
        /// Gets the only instance of the "ShellViewModel".
        /// </summary>
        public ShellViewModel ShellViewModel { get; private set; }

        /// <summary>
        /// Gets the viewmodel container instance.
        /// </summary>
        public static ViewModelContainer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new ViewModelContainer();
                        }
                    }
                }
                return _Instance;
            }
        }

        #endregion

        #region :: Constructor ::

        ViewModelContainer()
        {
            ShellViewModel = new ShellViewModel();
        }

        #endregion
       
    }

    /// <summary>
    /// Class ViewModelController
    /// </summary>
    public class ViewModelController
    {
        public ViewModelContainer Models
        {
            get
            {
                return ViewModelContainer.Instance;
            }
        }
    }
}
