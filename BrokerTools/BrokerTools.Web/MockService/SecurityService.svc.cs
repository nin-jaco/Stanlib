using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BrokerTools.Web.MockService
{
   [ServiceBehavior(Name = "WcfFundService",
                    Namespace = "urn:Stanlinb.BrokerTools",
                    IncludeExceptionDetailInFaults = true)]
    public class SecurityService : ISecurityService
    {
        string[] ISecurityService.GetUserFeatures(int stanlibID)
        {
            List<String> Features = new List<String>();

            Features.Add("Fund Construction Tool");

            return Features.ToArray();
        }
    }
}
