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
using System.Collections.Generic;
using BrokerTools.Data.Model;
using BrokerTools.Data.FundProxy;
using System.Threading;
using System.ServiceModel;
using BrokerTools.Data.Infrastructure;
using Sqo;
using dbTransaction = Sqo.Transactions;
using System.Linq;

namespace BrokerTools.Data
{
    public class BuildDBRepository : Repository
    {

        /// <summary>
        /// Check DB Initialized
        /// </summary>
        public bool HasInitialized
        {
            get
            {
                return siaqodb.LoadAll<Fund>().Count > 0;
            }
        }

        /// <summary>
        /// Cache lookup Tables
        /// </summary>
        public void BuildCache()
        {
            try
            {
               transaction = siaqodb.BeginTransaction();

                CacheProduct();

                CacheRiskProfile();

                transaction.Commit();
            }
            catch (Exception Ex)
            {
                transaction.Rollback();
                throw Ex;
            }

        }

        /// <summary>
        /// Cache Product
        /// </summary>
        void CacheProduct()
        {
            
            Product product = new Product()
            {
                Code = ProductCode.IP,
                Name = "Classic Investment Plan"
            };

            //Save object
            Save<Product>(product, transaction);

            product = new Product()
            {
                Code = ProductCode.PPR,
                Name = "Classic Retirement Annuity"
            };

            //Save object
            Save<Product>(product, transaction);

            product = new Product()
            {
                Code = ProductCode.MGP,
                Name = "Classic Preservation Pension Fund"
            };

            //Save object
            Save<Product>(product, transaction);

            product = new Product()
            {
                Code = ProductCode.RA,
                Name = "Classic Preservation Provident Fund"
            };

            //Save object
            Save<Product>(product, transaction);

            product = new Product()
            {
                Code = ProductCode.MVC,
                Name = "Classic Linked Life Annuity"
            };

            //Save object
            Save<Product>(product, transaction);
        }
        
        /// <summary>
        /// Cache Risk Profile
        /// </summary>
        void CacheRiskProfile()
        {

            RiskProfile profile = new RiskProfile()
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
            };

            //Save object
            Save<RiskProfile>(profile,transaction);

            profile = new RiskProfile()
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
            };

            //Save object
            Save<RiskProfile>(profile, transaction);

            profile = new RiskProfile()
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
            };

            //Save object
            Save<RiskProfile>(profile, transaction);

            profile = new RiskProfile()
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
            };

            Save<RiskProfile>(profile, transaction);

            profile = new RiskProfile()
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
            };

            //Save object
            Save<RiskProfile>(profile, transaction);
        }

        /// <summary>
        /// Cache Investment Term
        /// </summary>
        void CacheInvestmentTerm()
        {
            InvestmentTerm investTerm = new InvestmentTerm()
            {
                SortOrder = 1,
                Description = "Short Term",
            };

            //Save object
            Save<InvestmentTerm>(investTerm, transaction);

            investTerm = new InvestmentTerm()
            {
                SortOrder = 2,
                Description = "Medium Term",
            };

            //Save object
            Save<InvestmentTerm>(investTerm, transaction);

            investTerm = new InvestmentTerm()
            {
                SortOrder = 3,
                Description = "Long Term",
            };

            //Save object
            Save<InvestmentTerm>(investTerm, transaction);
        }

        /// <summary>
        /// Get Last Sync Date
        /// </summary>
        /// <returns></returns>
        public SyncLog GetLastSync()
        {
            return siaqodb.Cast<SyncLog>().SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetSyncNow()
        {
            var sl =siaqodb.Cast<SyncLog>().FirstOrDefault();
            if (sl == null)
                sl = new SyncLog();

            sl.LastSync = DateTime.Now;

            Save<SyncLog>(sl);
        }
    }
}
