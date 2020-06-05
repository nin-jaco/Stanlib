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
using System.IO.IsolatedStorage;

namespace BrokerTools.Views
{
    public partial class SetIsolatedStorageView : UserControl
    {
        public SetIsolatedStorageView()
        {
            InitializeComponent();
        }

        private void SetIsoStorageButton_Click(object sender, RoutedEventArgs e)
        {
            using (var iso = IsolatedStorageFile.GetUserStoreForApplication())
            {

                bool result = iso.IncreaseQuotaTo((1024 * 1024) * 100);
                MessageBox.Show("Done");
            }
        }
    }
}
