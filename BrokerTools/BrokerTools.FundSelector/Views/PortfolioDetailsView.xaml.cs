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
using BrokerTools.FundSelector.ViewModels;
using BrokerTools.FundSelector.Controllers;
using BrokerTools.Data.Model;
using BrokerTools.Data.Controller;
using BrokerTools.Data.FundProxy;

namespace BrokerTools.FundSelector.Views
{
    public partial class PortfolioDetailsView : UserControl
    {
        public PortfolioDetailsView()
        {
            InitializeComponent();

            this.DataContext = ViewModelController.Models.SharedViewModel; 

        }
    }
}
