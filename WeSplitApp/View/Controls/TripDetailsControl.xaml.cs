using System.Windows;
using System.Windows.Controls;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for TripDetailsControl.xaml
    /// </summary>
    public partial class TripDetailsControl : UserControl
    {
        public TripDetailsControl()
        {
            InitializeComponent();
            HomeScreen.GetHomeScreenInstance().AddButton.Visibility = Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().AddButton.Visibility = Visibility.Visible;
        }
    }
}
