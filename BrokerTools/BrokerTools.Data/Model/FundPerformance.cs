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
using Sqo.Attributes;

namespace BrokerTools.Data.Model
{
    public class FundPerformance
    {
        public int OID { get; set; }

        [Ignore]
        public int FundOID { get; set; }

        public string FundID { get; set; }

        public DateTime Date { get; set; }

        public double Performance { get; set; }

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
