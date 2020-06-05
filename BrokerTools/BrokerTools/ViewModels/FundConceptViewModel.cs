
using System;
using Microsoft.Practices.Prism.ViewModel;
using BrokerTools.Model;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using BrokerTools.Data;
namespace BrokerTools.ViewModels
{
    public class FundConceptViewModel:NotificationObject
    {
        public FundConcept fund 
        {
            get;
            private set;
        }




        public DelegateCommand<object> SaveFundCommand { get; private set; }

        public FundConceptViewModel()
        {
            fund = new FundConcept();
            this.SaveFundCommand = new DelegateCommand<object>(this.Save, this.CanSave);
        }

        private bool CanSave(object arg)
        {
            return !this.fund.HasErrors;
        }

        private void Save(object obj)
        {

            fund.Validate();

            if (fund.HasErrors)
                MessageBox.Show("Fund Errors");
            else
            {
                using (FundRepository fac = new FundRepository())
                {
                    fac.Save<FundConcept>(fund);
                }
            }
        }
    }
}
