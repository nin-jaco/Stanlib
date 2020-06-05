using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FundSelectorViews = BrokerTools.FundSelector.Views;
using Telerik.Windows.Controls;
using System.Windows.Data;

namespace BrokerTools.Views
{
    public partial class ShellView : UserControl
    {
        bool _SliderHasBeenRefreshed = false;
        double _PreviousSliderValue = 0.0;

        public ShellView()
        {
            InitializeComponent();
        }

        private void StepIndicator_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RadSlider slider = sender as RadSlider;
            if (slider.DataContext != null)
            {
                if (_PreviousSliderValue != slider.Value)
                {
                    _SliderHasBeenRefreshed = false;
                    _PreviousSliderValue = slider.Value;
                }

                if (!_SliderHasBeenRefreshed)
                {
                    _SliderHasBeenRefreshed = true;
                    object sliderDataContext = slider.DataContext;
                    slider.DataContext = null;
                    slider.DataContext = sliderDataContext;
                }
            }
        }
    }
}
