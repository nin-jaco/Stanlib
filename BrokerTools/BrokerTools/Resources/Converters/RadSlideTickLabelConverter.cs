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
using BrokerTools.Controllers;


namespace BrokerTools.Resources.Converters
{
    public class RadSlideTickLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valueAsString = value.ToString();
            if(valueAsString.Contains("."))
                valueAsString = valueAsString.Substring(0, valueAsString.IndexOf('.'));

            ViewModelController viewModelController = App.Current.Resources["ViewModelController"] as ViewModelController;
            return viewModelController.Models.ShellViewModel.CurrentToolStepLabels[int.Parse(valueAsString)-1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
