using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            MEMBER byMember = ByMemberListComboBox.SelectedItem as MEMBER;
            if (member == null)
            {
                MessageBox.Show("Bạn chưa chọn Thành viên"); return;
            }
            string cost = MemberAmountpaidTextBox.Text;
            if (cost == null)
            {
                MessageBox.Show("Bạn chưa nhập tiền thu"); return;
            }
            if (!cost.All(char.IsDigit))
            {
                MessageBox.Show("Tiền thu phải là Số ( vd: 10000)"); return;
            }
            if (AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Where(item => item.MEMBER_ID == member.MEMBER_ID).ToList().Count != 0) //TODO 
            {
                MessageBox.Show("Đã có thành viên này !");
                MemberListComboBox.SelectedItem = null;
                return;
            }
            if(byMember == null)
                AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Add(new TRIP_MEMBER { MEMBER_ID = member.MEMBER_ID, AMOUNTPAID = Convert.ToDouble(cost), MEMBER = member });
            else
            {
                AddNewTripViewModel.Instance.AddTrip.TRIP_MEMBER.Add(new TRIP_MEMBER { MEMBER_ID = member.MEMBER_ID, AMOUNTPAID = Convert.ToDouble(cost), MEMBER = member, BYMEMBER_ID = byMember.MEMBER_ID, MEMBER1 = byMember });
            }
            MemberAddedListView.Items.Refresh();

            TotalPayAmountTextBox.Text = (Convert.ToInt32(TotalPayAmountTextBox.Text) + Convert.ToInt32(cost)).ToString();
            AddNewTripViewModel.Instance.AddTrip.CURRENTPROCEEDS = Convert.ToDouble(TotalPayAmountTextBox.Text);
            MemberListComboBox.SelectedItem = null;
            ByMemberListComboBox.SelectedItem = null;
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            bool AddOk = StepThreeViewModel.isAddOk();
            if (AddOk)
            {
                HomeScreen.RestoreNavigationItem();
                HomeScreen.GetHomeScreenInstance().setVisibilityAddButton(Visibility.Visible);
            }
        }

        private void MemberAddButton_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().GetDialogs("MemberAddDialog", new MEMBER(),"THÊM THÀNH VIÊN");
        }

        private void MemberListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MEMBER member = MemberListComboBox.SelectedItem as MEMBER;
            MEMBER byMember = ByMemberListComboBox.SelectedItem as MEMBER;
            if (member == byMember && member != null && byMember != null)
            {
                MessageBox.Show("Không được chọn trùng Thành viên với Thành viên trả trước !");
                MemberListComboBox.SelectedItem = null;
            }
        }
    }
}
