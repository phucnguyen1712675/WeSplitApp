using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : MaterialWindow
    {
        public SplashScreen() => InitializeComponent();

        public double ProgressBar
        {
            get => this.progressBar.Value;
            set => this.progressBar.Value = value;
        }

        private void DoNotShowPlashScreenCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            var result = (!this.showAgainCheckBox.IsChecked).ToString();

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["isSplashScreenAllowed"].Value = result;
            config.Save(ConfigurationSaveMode.Minimal);
        }
    }
}
