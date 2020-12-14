using System;
using System.Windows;
using System.Windows.Controls;
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for LocationListControl.xaml
    /// </summary>
    public partial class LocationListControl : UserControl
    {
        private static LocationListControl instance { get; set; }
        public LocationListControl()
        {
            InitializeComponent();
            instance = this;
        }

        private void EditLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Button editLocationButton = sender as Button;
            LOCATION editLocation = editLocationButton.Tag as LOCATION;

            HomeScreen.GetHomeScreenInstance().GetDialogs("LocationAddDialog", editLocation, "Xem/Chỉnh sửa điểm dừng");
        }

        internal static void updateUI()
        {
            instance.LocationList.Items.Refresh();
        }
    }
}
