using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sqo;
using Sqo.Attributes;
using System.ComponentModel.DataAnnotations;
using BrokerTools.Data;
using BrokerTools.Data.FundProxy;
using System.Collections.ObjectModel;

namespace BrokerTools.Data.Model
{
    public class Product
    {
        public int OID { get; set; }

        public ProductCode Code { get; set; }

        public string Name { get; set; }

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