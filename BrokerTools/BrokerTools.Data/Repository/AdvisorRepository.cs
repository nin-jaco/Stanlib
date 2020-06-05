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
    public class AdvisorRepository : Repository
    {
        public IEnumerable<Advisor> GetAllAdvisors()
        {
            return siaqodb.LoadAll<Advisor>();
        }
    }
}
