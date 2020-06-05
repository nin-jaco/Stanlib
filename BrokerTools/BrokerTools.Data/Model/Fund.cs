using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sqo;
using Sqo.Attributes;
using System.ComponentModel.DataAnnotations;
using BrokerTools.Data;
using BrokerTools.Data.FundProxy;

namespace BrokerTools.Data.Model
{
    public partial class Fund
    {
        public int OID { get; set; }
      
        /// <summary>
        /// Ignore
        /// </summary>
        [Ignore]
        public int ProductOID { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [Ignore]
        public List<FundPerformance> PerformanceList { get; set; }


        public string ID { get; set; }

        public ProductCode ProductCode { get; set; }

        public string FullName { get; set; }

        public double Resources { get; set; }

        public double Financials { get; set; }

        public double Industrials { get; set; }

        public double Equity { get; set; }

        public double AltX { get; set; }

        public double Property { get; set; }

        public double Bonds { get; set; }

        public double Cash { get; set; }

        public double TotalLocal { get; set; }

        public double ForeignEquity { get; set; }

        public double ForeignBonds { get; set; }

        public double ForeignProperty { get; set; }

        public double ForeignCash { get; set; }

        public double FundRisk { get; set; }

        public int RiskRating { get; set; }



    }
}