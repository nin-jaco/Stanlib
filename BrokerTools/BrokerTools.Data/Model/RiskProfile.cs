using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sqo;
using Sqo.Attributes;
using System.ComponentModel.DataAnnotations;
using BrokerTools.Data;

namespace BrokerTools.Data.Model
{
    public class RiskProfile : DbDomainObject
    {
        public int OID { get; set; }

        public string Description { get; set; }

        public double EquityMinValue { get; set; }
        public double EquityMaxValue { get; set; }

        public double PropertyMinValue { get; set; }
        public double PropertyMaxValue { get; set; }

        public double BondsMinValue { get; set; }
        public double BondsMaxValue { get; set; }

        public double CashMinValue { get; set; }
        public double CashMaxValue { get; set; }

        public int RiskLevel { get; set; }

        [Ignore]
        public string DescriptionUpperCase 
        {
            get
            {
                return Description.ToUpper();
            }
        }

        public object GetValue(System.Reflection.FieldInfo field)
        {
            return field.GetValue(this);
        }

        public void SetValue(System.Reflection.FieldInfo field, object value)
        {
            field.SetValue(this, value);
        }

    }
}