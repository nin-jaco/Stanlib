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
using System.Threading;

namespace BrokerTools.SharedUIResources.Controls
{
    public class FinancialTextBox : TextBox
    {
        public FinancialTextBox()
            : base()
        {
            this.Style = Application.Current.Resources["FinancialTextBoxStyle"] as Style;
            Loaded += new RoutedEventHandler(FinancialTextBox_Loaded);
            this.MaxLength = decimal.MaxValue.ToString().Length - 1;
        }

        void FinancialTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock currencyIndicator = this.GetTemplateChild("CurrencyIndicator") as TextBlock;
            currencyIndicator.Text = "R";
            TextBox valueTextBox = (sender as TextBox);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            TextBox valueTextBox = (TextBox)e.OriginalSource;
            if (string.IsNullOrEmpty(valueTextBox.Text))
                valueTextBox.Text = "0.00";
            valueTextBox.SelectionStart = 0;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            TextBox valueTextBox = (TextBox)e.OriginalSource;

            if (!string.IsNullOrEmpty(valueTextBox.Text.Trim()))
            {
                if (valueTextBox.Text.EndsWith("."))
                    valueTextBox.Text += "00";
                if (!valueTextBox.Text.Contains("."))
                    valueTextBox.Text += ".00";

                valueTextBox.Text = string.Format("{0:0.00}", Double.Parse(valueTextBox.Text));
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            TextBox valueTextBox = (TextBox)e.OriginalSource;
            if (e.Key == Key.Unknown)
            {
                decimal value = 0.0m;
                if (!decimal.TryParse(valueTextBox.Text, out value))
                {
                    int caretIndex = valueTextBox.SelectionStart - 1;
                    valueTextBox.Text = valueTextBox.Text.Remove(caretIndex, 1);
                    valueTextBox.SelectionStart = caretIndex;
                }
            }

            //if (valueTextBox.Text.Length == 0)
            //    valueTextBox.Text = "0.00";
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            TextBox valueTextBox = (TextBox)e.OriginalSource;

            if (((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key == Key.Decimal || e.Key == Key.Unknown) ||
                e.Key == Key.Tab) &&
                (Keyboard.Modifiers & ModifierKeys.Shift) == 0 &&
                (Keyboard.Modifiers & ModifierKeys.Control) == 0)
            {
                if (e.Key == Key.Decimal || e.Key == Key.Unknown)
                {
                    if (valueTextBox.Text.Contains(".") || e.PlatformKeyCode == 188)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                if (valueTextBox.Text.Contains("."))
                {
                    if (valueTextBox.SelectionStart > valueTextBox.Text.IndexOf("."))
                    {
                        if (string.IsNullOrEmpty(valueTextBox.SelectedText))
                        {
                            string decimalValue = valueTextBox.Text.Substring(valueTextBox.Text.IndexOf(".") + 1);
                            if (decimalValue.Length == 2)
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                }

                e.Handled = false;
                return;
            }

            e.Handled = true;
        }
    }
}
