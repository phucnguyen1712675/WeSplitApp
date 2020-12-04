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
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls.AddTripSteps
{
    /// <summary>
    /// Interaction logic for StepTwoControl.xaml
    /// </summary>
    public partial class StepTwoControl : UserControl
    {
        public StepTwoControl()
        {
            InitializeComponent();
            //LocationListComboBox.ItemsSource = HomeScreen.homeScreen.database.LOCATIONS.ToList();
            CostIncurredListComboBox.ItemsSource = HomeScreen.homeScreen.database.COSTINCURREDS.ToList();
            this.DataContext = AddNewTripViewModel.Instance;
        }

        private void LocationAddToTripButton_Click(object sender, RoutedEventArgs e)
        {
            LOCATION location = LocationListComboBox.SelectedItem as LOCATION;
            if (location == null)
            {
                MessageBox.Show("Bạn chưa chọn địa điểm dừng"); return;
            }
            string cost = LocationCostTextBox.Text;
            if (cost == null)
            {
                MessageBox.Show("Bạn chưa nhập Chi phí"); return;
            }
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_LOCATION.Where(item => item.LOCATION_ID == location.LOCATION_ID).ToList().Count != 0) //TODO 
            {
                MessageBox.Show("Đã có điểm dừng này !"); return;
            }
            AddNewTripViewModel.Instance.AddTrip.TRIP_LOCATION.Add(new TRIP_LOCATION { LOCATION_ID = location.LOCATION_ID, COSTS = Convert.ToDouble(cost), LOCATION = location});
            LocationAddedListView.Items.Refresh();

            TotalCostTextBox.Text =( Convert.ToInt32(TotalCostTextBox.Text) + Convert.ToInt32(cost)).ToString();
            AddNewTripViewModel.Instance.AddTrip.TOTALCOSTS = Convert.ToDouble(TotalCostTextBox.Text);
        }

        private void CostIncureedAddToTripButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            COSTINCURRED CostIncurred = CostIncurredListComboBox.SelectedItem as COSTINCURRED;
            if (CostIncurred == null)
            {
                MessageBox.Show("Bạn chưa chọn địa Loại chi phí"); return;
            }
            string cost = CostIncurredCostTextBox.Text;
            if (cost == null)
            {
                MessageBox.Show("Bạn chưa nhập Chi phí"); return;
            }
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_COSTINCURRED.Where(item => item.COST_ID == CostIncurred.COST_ID).ToList().Count != 0) //TODO 
            {
                MessageBox.Show("Đã có chi phí này !"); return;
            }
            AddNewTripViewModel.Instance.AddTrip.TRIP_COSTINCURRED.Add(new TRIP_COSTINCURRED { COST_ID = CostIncurred.COST_ID, COST = Convert.ToDouble(cost), COSTINCURRED = CostIncurred });
            CostIncurredAddedListView.Items.Refresh();

            TotalCostTextBox.Text = (Convert.ToInt32(TotalCostTextBox.Text) + Convert.ToInt32(cost)).ToString();
            AddNewTripViewModel.Instance.AddTrip.TOTALCOSTS = Convert.ToDouble(TotalCostTextBox.Text);
        }

        private void LocationAddButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.homeScreen.GetDialogs("LocationAddDialog", new LOCATION(),"THÊM ĐIỂM DỪNG");
        }
    }
}
