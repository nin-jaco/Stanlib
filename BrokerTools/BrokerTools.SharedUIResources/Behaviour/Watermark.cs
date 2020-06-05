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
using System.Windows.Interactivity;

namespace BrokerTools.SharedUIResources.Behaviour
{
    public class Watermark : Behavior<TextBox>
    {
        private bool _hasWatermark;
        private Brush _textBoxForeground;

        public String Text { get; set; }
        public Brush Foreground { get; set; }

        protected override void OnAttached()
        {
            _textBoxForeground = AssociatedObject.Foreground;

            base.OnAttached();
            if (Text != null)
                SetWatermarkText();
            AssociatedObject.GotFocus += GotFocus; AssociatedObject.LostFocus += LostFocus;
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.Text.Length == 0)
                if (Text != null) SetWatermarkText();
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (_hasWatermark) RemoveWatermarkText();
        }

        private void RemoveWatermarkText()
        {
            AssociatedObject.Foreground = _textBoxForeground;
            AssociatedObject.Text = ""; _hasWatermark = false;
        } 
        
        private void SetWatermarkText() 
        {
            AssociatedObject.Foreground = Foreground;
            AssociatedObject.Text = Text;
            _hasWatermark = true;
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching(); 
            AssociatedObject.GotFocus -= GotFocus; 
            AssociatedObject.LostFocus -= LostFocus;
        }
    }

    public class WatermarkCombo : Behavior<ComboBox>
    {
        private bool _hasWatermark;
        private Brush _textBoxForeground;

        public String Text { get; set; }
        public Brush Foreground { get; set; }

        protected override void OnAttached()
        {
            _textBoxForeground = AssociatedObject.Foreground;

            base.OnAttached();
            if (Text != null)
                SetWatermarkText();
            AssociatedObject.GotFocus += GotFocus; AssociatedObject.LostFocus += LostFocus;
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if(AssociatedObject.SelectedItem == null)
                SetWatermarkText();
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (_hasWatermark) RemoveWatermarkText();
        }

        private void RemoveWatermarkText()
        {
            AssociatedObject.Foreground = _textBoxForeground;
            AssociatedObject.Items.Remove(0);
            _hasWatermark = false;
        }

        private void SetWatermarkText()
        {
            AssociatedObject.Foreground = Foreground;
            AssociatedObject.Items.Insert(0,new ComboBoxItem(){Foreground=Foreground,Content=Text,IsSelected=true,Tag="-1"});
            AssociatedObject.SelectedIndex = 0;
            _hasWatermark = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= GotFocus;
            AssociatedObject.LostFocus -= LostFocus;
        }
    }

    public class WatermarkAutoComplete : Behavior<AutoCompleteBox>
    {
        private bool _hasWatermark;
        private Brush _textBoxForeground;

        public String Text { get; set; }
        public Brush Foreground { get; set; }

        protected override void OnAttached()
        {
            _textBoxForeground = AssociatedObject.Foreground;

            base.OnAttached();
            if (Text != null)
                SetWatermarkText();
            AssociatedObject.GotFocus += GotFocus; AssociatedObject.LostFocus += LostFocus;
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.Text.Length == 0)
                if (Text != null) SetWatermarkText();
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (_hasWatermark) RemoveWatermarkText();
        }

        private void RemoveWatermarkText()
        {
            AssociatedObject.Foreground = _textBoxForeground;
            AssociatedObject.Text = ""; _hasWatermark = false;
        }

        private void SetWatermarkText()
        {
            AssociatedObject.Foreground = Foreground;
            AssociatedObject.Text = Text;
            _hasWatermark = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= GotFocus;
            AssociatedObject.LostFocus -= LostFocus;
        }
    }
}
