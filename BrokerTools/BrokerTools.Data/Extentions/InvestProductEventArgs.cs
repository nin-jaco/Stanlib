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
using BrokerTools.Data.FundProxy;

namespace BrokerTools.Data.Extentions
{
    public class InvestProductEventArgs:EventArgs
    {   
        private InvestmentProduct investmentproduct; 

        public InvestProductEventArgs(InvestmentProduct ip) {
            this.investmentproduct = ip; 
        }

        public InvestmentProduct InvestmentProduct
        {
            get
            {

                return investmentproduct;
            }
        }
    }

    public class SourceLookupEventArgs : EventArgs
    {
        private SourceLookup sourcelookup;

        public SourceLookupEventArgs(SourceLookup lookup)
        {
            this.sourcelookup = lookup;
        }

        public SourceLookup SourceLookup
        {
            get
            {

                return sourcelookup;
            }
        }
    }
}
