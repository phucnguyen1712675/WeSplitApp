using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for StepThreeControl.xaml
    /// </summary>
    public partial class StepThreeControl : UserControl
    {
        public StepThreeControl()
        {
            InitializeComponent();
            //MemberListComboBox.ItemsSource = HomeScreen.GetDatabaseEntities().MEMBERS.ToList();
            this.DataContext = AddNewTripViewModel.Instance;
        }

        private void MemberAddToTripButton_Click(object sender, RoutedEventArgs e)
        {
            MEMBER member = MemberListComboBox.SelectedItem as MEMBER;
            if (member == null)
            {
                MessageBox.Show("Bạn chưa chọn Thành viên"); return;
            }
            string cost = MemberAmountpaidTextBox.Text;
            if (cost == null)
            {
                MessageBox.Show("Bạn chưa nhập tiền thu"); return;
            }
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Where(item => item.MEMBER_ID == member.MEMBER_ID).ToList().Count != 0) //TODO 
            {
                MessageBox.Show("Đã có thành viên này !"); return;
            }
            AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Add(new TRIP_MEMBER { MEMBER_ID = member.MEMBER_ID, AMOUNTPAID = Convert.ToDouble(cost), MEMBER = member });
            MemberAddedListView.Items.Refresh();

            TotalPayAmountTextBox.Text = (Convert.ToInt32(TotalPayAmountTextBox.Text) + Convert.ToInt32(cost)).ToString();
            AddNewTripViewModel.Instance.AddTrip.CURRENTPROCEEDS = Convert.ToDouble(TotalPayAmountTextBox.Text);
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            bool AddOk = StepThreeViewModel.isAddOk();
            if (AddOk)
            {
                HomeScreen.GetHomeScreenInstance().SetContentControl(new HaveTakenTripsListViewViewModel());
                HomeScreen.GetHomeScreenInstance().setVisibilityAddButton(Visibility.Visible);
            }
        }

        public static void UpdateList()
        {

        }

        private void MemberAddButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().GetDialogs("MemberAddDialog", new MEMBER(),"THÊM THÀNH VIÊN");
        }
    }
}
