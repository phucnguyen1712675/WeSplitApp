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
using WeSplitApp.View.Controls;

namespace WeSplitApp.View.DialogHelper
{
    /// <summary>
    /// Interaction logic for AddLocationDialog.xaml
    /// </summary>
    public partial class LocationAddDialog : UserControl
    {
        public LocationAddDialog()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            int locationId = Convert.ToInt32((sender as Button).Tag);
            if (locationId != 0)
            {
                LOCATION reloadLocation = HomeScreen.homeScreen.database.LOCATIONS.FirstOrDefault(item => item.LOCATION_ID == locationId);
                HomeScreen.homeScreen.database.Entry(reloadLocation).Reload();
                LocationListControl.updateUI();
            }
        }
    }
}
