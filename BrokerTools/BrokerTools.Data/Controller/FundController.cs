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
using BrokerTools.Data.Model;
using System.Collections;
using System.Collections.Generic;
using BrokerTools.Data.FundProxy;

namespace BrokerTools.Data.Controller
{
    public class FundController
    {
        public static void CreateFundWithPerformance(Fund fund)
        {
            using (FundRepository db = new FundRepository())
            {
                db.CreateFundWithPerformance(fund);
            }
        }

        public static void ClearFundData()
        {
            using (FundRepository db = new FundRepository())
            {
                db.ClearFundData();
            }
        }

        public static IEnumerable<Fund> GetFundByProductCode(ProductCode productCode)
        {
            using (FundRepository db = new FundRepository())
            {
               return db.GetFundsByProductCode(productCode);
            }
        }
    }
}
