using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrokerTools.Views;
using BrokerTools.Data.Controller;

namespace BrokerTools.Controllers
{
    /// <summary>
    ///     Simple navigation class
    /// </summary>
    public class Navigation
    {
        private readonly Action<UserControl> _setView;
        private readonly Lazy<UserControl> _buildView = new Lazy<UserControl>(()=>new BuildView());
        private readonly Lazy<UserControl> _shellView = new Lazy<UserControl>(() => new ShellView());

        /// <summary>
        ///     Navigation
        /// </summary>
        /// <param name="setView">Set the view</param>
        public Navigation(Action<UserControl> setView)
        {
            _setView = setView;
            _BeginNavigation();
        }

        /// <summary>
        ///     Begin the navigation
        /// </summary>
        private void _BeginNavigation()
        {
            if (BuildDBController.ShouldSync())
                NavigateToBuildView();
            else
                NavigateToShellView();
        }

        public void NavigateToShellView()
        {
            _setView(_shellView.Value);
        }

        public void NavigateToBuildView()
        {
            _setView(_buildView.Value);
            ShellViewService.Current.RequestRebuild();
        }

    }
}
