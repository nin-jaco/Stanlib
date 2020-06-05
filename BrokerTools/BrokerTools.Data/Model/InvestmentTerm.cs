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

namespace BrokerTools.Data.Model
{
    public class InvestmentTerm
    {
        public int OID { get; set; }

        public int SortOrder { get; set; }

        public string Description { get; set; }

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
