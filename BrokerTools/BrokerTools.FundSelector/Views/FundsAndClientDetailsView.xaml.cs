using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrokerTools.FundSelector.Controllers;

namespace BrokerTools.FundSelector.Views
{
    public partial class FundsAndClientDetailsView : UserControl
    {
        public FundsAndClientDetailsView()
        {
            InitializeComponent();
        }

        private void PoenerseHOES_Click(object sender, RoutedEventArgs e)
        {
            ViewNavigationService.Instance.ViewNavigator.NavigateToPortfolioDetails();
        }
    }
}
