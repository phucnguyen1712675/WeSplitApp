using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
