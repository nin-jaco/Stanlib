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
using System.Collections.Generic;
using System.Linq;
using BrokerTools.Data.FundProxy;

namespace BrokerTools.Data.Controller
{
    public class RiskProfileController
    {

        /// <summary>
        /// Get All Risk Profile
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public static IEnumerable<RiskProfile> GetAllRiskProfile()
        {
            using (RiskProfileRepository db = new RiskProfileRepository())
            {
                return db.GetAllRiskProfile();
            }
        }


        /// <summary>
        /// Get Risk Profile by level
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public static RiskProfile GetRiskProfileByLevel(int level)
        {
            using (RiskProfileRepository db = new RiskProfileRepository())
            {
                return db.GetRiskProfileByLevel(level);
            }
        }
    }
}
