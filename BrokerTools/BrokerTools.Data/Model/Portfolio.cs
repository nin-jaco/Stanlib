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
    public partial class Portfolio
    {
        public int OID { get; set; }

        public string PortfolioNumber { get; set; }

        public float CGTExemption { get; set; }

        public double MarginalTaxRate { get; set; }

        public double CGTInclusionRate { get; set; }

        public float InvestmentAmount { get; set; }

        public float InterestExemption { get; set; }

        /// <summary>
        /// Foreign Keys
        /// </summary>
        public int AdvisorOID { get; set; }

        public int ProductOID { get; set; }

        /// <summary>
        /// Client Risk Profile
        /// </summary>
        public int RiskProfileOID { get; set; }

        public int InvestmentTermOID { get; set; }

        /// <summary>
        /// Risk based on Funds Selected
        /// </summary>
        public int PortfolioRiskOID { get; set; }

        /// <summary>
        /// Cash Flow Analysis Details
        /// </summary>
        public Nullable<int> Age { get; set; }

        public Nullable<double> AnnualGrowthRate { get; set; }

        public Nullable<double> GroupInitialFee { get; set; }

        public Nullable<double> BrokerInitialFee { get; set; }

        public Nullable<double> BrokerOngoingFee { get; set; }

        public Nullable<float> IncomeAmount { get; set; }

        public Nullable<float> IncomeGrowthRate { get; set; }

        public Nullable<double> PlatformServiceFee { get; set; }

        public Nullable<double> BrokerServiceFee { get; set; }

        public Nullable<double> AssetAllocationFee { get; set; }


    }
}