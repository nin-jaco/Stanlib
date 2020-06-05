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
    public partial class PortfolioFund:DbDomainObject
    {
        public int OID { get; set; }
      
        /// <summary>
        /// Ignore
        /// </summary>
        [Ignore]
        public int PortfolioOID { get; set; }


        private int FundOID;

        [UseVariable("FundOID")]
        public int fundOID
        {
            get
            {
                return this.FundOID;
            }
            set
            {
                this.FundOID = value;
                NotifyPropertyChanged(() => fundOID);
            }
        }

        [Ignore]
        private Fund fund;

        [Ignore]
        public Fund Fund
        {
            get
            {
                return this.fund;
            }
            set
            {
                this.fund = value;
                NotifyPropertyChanged(() => Fund);
            }
        }

        //[Ignore]
        //public Fund Fund 
        //{
        //    get
        //    {
        //        return this.fund;
        //    }
        //    set
        //    {
        //        this.fund = value;
        //        NotifyPropertyChanged(() => Fund);
        //    }
        //}

        private double Weighting;

        [UseVariable("Weighting")]
        [Required(ErrorMessage="{0} is Required}")]
        [Range(1,99,ErrorMessage="The value must be between 1 and 99")]
        public double weighting
        {
            get
            {
                return this.Weighting;
            }
            set
            {
                this.Weighting = value;
                NotifyPropertyChanged(() => weighting);
            }
        }
    }
}