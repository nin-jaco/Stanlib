using System;
using System.Linq;
using Sqo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BrokerTools.Model;
using System.Threading;
using BrokerTools.Data.Infrastructure;
using BrokerTools.Data.FundProxy;
using System.ServiceModel;
using BrokerTools.Data.Model;

namespace BrokerTools.Data
{
    public class RiskProfileRepository : Repository
    {
        /// <summary>
        /// Get All Risk Profile
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RiskProfile> GetAllRiskProfile()
        {
            return siaqodb.LoadAll<RiskProfile>();
        }

        /// <summary>
        /// Get Risk Profile by level
        /// </summary>
        /// <returns></returns>
        public RiskProfile GetRiskProfileByLevel(int level)
        {
            return siaqodb.Cast<RiskProfile>().SingleOrDefault(t => t.RiskLevel == level);
        }
    }
}
