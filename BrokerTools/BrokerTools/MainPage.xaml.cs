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
using BrokerTools.Model;
using System.IO.IsolatedStorage;
using System.ComponentModel.DataAnnotations;
using Sqo.Transactions;
using Sqo;
using BrokerTools.Data;
using BrokerTools.Data.Model;

namespace BrokerTools
{
    public partial class MainPage : UserControl
    {
        const string HAPPY = "Thank you! The quota has been increased.";
        const string SAD = "The quota was not increased. The application may fail.";

        const long quotasize = (1024 * 1024) * 100;


        public MainPage()
        {
            InitializeComponent();
        }

        private void oob_Click(object sender, RoutedEventArgs e)
        {

            Siaqodb instance = SiaqodbFactory.GetInstance();
            try
            {
                //Fund fund = new Fund() { productcode = "UT", fullname = "STANLIB Industrial Fund A" };
                List<RiskProfile> profiles = new List<RiskProfile>();

                profiles.Add(new RiskProfile()
                {
                    Description = "Conservative",
                    EquityMinValue = 0.05d,
                    EquityMaxValue = 0.2d,

                    PropertyMinValue = 0d,
                    PropertyMaxValue = 0.2d,

                    BondsMinValue = 0.2d,
                    BondsMaxValue = 0.3d,

                    CashMinValue = 0.4d,
                    CashMaxValue = 0.6d
                });


                profiles.Add(new RiskProfile()
                {
                    Description = "Moderately Conservative",
                    EquityMinValue = 0.3d,
                    EquityMaxValue = 0.4d,

                    PropertyMinValue = 0d,
                    PropertyMaxValue = 0.2d,

                    BondsMinValue = 0.2d,
                    BondsMaxValue = 0.3d,

                    CashMinValue = 0.2d,
                    CashMaxValue = 0.4d
                });

                profiles.Add(new RiskProfile()
                {
                    Description = "Moderate",
                    EquityMinValue = 0.45d,
                    EquityMaxValue = 0.55d,

                    PropertyMinValue = 0d,
                    PropertyMaxValue = 0.2d,

                    BondsMinValue = 0.2d,
                    BondsMaxValue = 0.3d,

                    CashMinValue = 0.15d,
                    CashMaxValue = 0.25d
                });

                profiles.Add(new RiskProfile()
                {
                    Description = "Moderately Aggressive",
                    EquityMinValue = 0.60d,
                    EquityMaxValue = 0.75d,

                    PropertyMinValue = 0d,
                    PropertyMaxValue = 0.2d,

                    BondsMinValue = 0.1d,
                    BondsMaxValue = 0.2d,

                    CashMinValue = 0.5d,
                    CashMaxValue = 0.15d
                });

                profiles.Add(new RiskProfile()
                {
                    Description = "Aggressive",
                    EquityMinValue = 0.75d,
                    EquityMaxValue = 0.95d,

                    PropertyMinValue = 0d,
                    PropertyMaxValue = 0.2d,

                    BondsMinValue = 0d,
                    BondsMaxValue = 0.1d,

                    CashMinValue = 0d,
                    CashMaxValue = 0.1d
                });

               Transaction transaction = instance.BeginTransaction();
               try
               {
                   for (int i = 0; i < profiles.Count; i++)
                   {
                       instance.StoreObject(profiles[i]);
                   }
                   transaction.Commit();
               }
               catch
               {
                   transaction.Rollback();
                   throw;
               }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                //transaction
                //instance.Close();
            }
        }

        void fund_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void quota_Click(object sender, RoutedEventArgs e)
        {
            using (var iso = IsolatedStorageFile.GetUserStoreForApplication())
            {

                bool result = iso.IncreaseQuotaTo(quotasize);
                MessageBox.Show(result  
                                    ? HAPPY
                                    : SAD);
            }
        }

    }
}
