using System.Windows.Controls;
using System;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrokerTools.FundSelector.Views;

namespace BrokerTools.FundSelector.Controllers
{
    public class ViewNavigationController
    {
        private readonly Action<UserControl> _setView;
        private readonly Lazy<UserControl> _FundsAndClientDetailsView = new Lazy<UserControl>(() => new FundsAndClientDetailsView());
        private readonly Lazy<UserControl> _PortfolioDetailsView = new Lazy<UserControl>(() => new PortfolioDetailsView());

        public ViewNavigationController(Action<UserControl> setView)
        {
            _setView = setView;
            BeginNavigation();
        }

        private void BeginNavigation()
        {
            NavigateToPortfolioDetails();
        }

        public void NavigateToFundsAndDetailsView()
        {
            _setView(_FundsAndClientDetailsView.Value);
        }

        public void NavigateToPortfolioDetails()
        {
            _setView(_PortfolioDetailsView.Value);
        }
    }
}
