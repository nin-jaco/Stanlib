using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrokerTools.Web
{

    /// <summary>
    /// Only for Development purposes. This will be supplied by the stanlib backend service on the GetFund Call.
    /// </summary>
    public partial class SourceLookup
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ProductCode { get; set; }
    }
}