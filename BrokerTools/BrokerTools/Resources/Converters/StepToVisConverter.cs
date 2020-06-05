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
using System.Windows.Data;

namespace BrokerTools.Resources.Converters
{
    public class StepToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double dVal = (double)value;
                int expectedStep;

                if (int.TryParse(parameter.ToString(), out expectedStep))
                {
                    return ((double)value == expectedStep) ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    bool pass = false;
                    if (parameter.ToString().Contains("|"))
                    {
                        string[] parameters = parameter.ToString().Split('|');

                        foreach (string parm in parameters)
                            if (ValidateValue(parm, dVal))
                            {
                                pass = true;
                                break;
                            }
                    }
                    else
                        pass = ValidateValue(parameter.ToString(), dVal);
                    return (pass) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        bool ValidateValue(string parameter, double value)
        {
            string opr = parameter.Substring(0, 1);
            int val;
            if (int.TryParse(parameter.Substring(1), out val))
            {
                switch (opr)
                {
                    case "=":
                        if (value != val)
                            return false;
                        break;
                    case "<":
                        if (value >= val)
                            return false;
                        break;
                    case ">":
                        if (value <= val)
                            return false;
                        break;
                }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
