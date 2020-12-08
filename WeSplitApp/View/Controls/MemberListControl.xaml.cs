using System;
using System.Windows;
using System.Windows.Controls;
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for MemberListControl.xaml
    /// </summary>
    public partial class MemberListControl : UserControl
    {
        private static MemberListControl instance { get; set; }

        public MemberListControl()
        {
            InitializeComponent();
            instance = this;
        }

        private void EditMemberButton_Click(object sender, RoutedEventArgs e)
        {
            Button editMemberButton = sender as Button;
            MEMBER editMember = editMemberButton.Tag as MEMBER;
            HomeScreen.GetHomeScreenInstance().GetDialogs("MemberAddDialog", editMember, "Xem/Chỉnh sửa Thành viên");
        }

        public static void updateMemberList()
        {
            instance.MemberList.Items.Refresh();
        }
    }
}
