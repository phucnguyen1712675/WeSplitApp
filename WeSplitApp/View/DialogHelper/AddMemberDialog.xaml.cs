using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WeSplitApp.View.Controls;
using Button = System.Windows.Controls.Button;
using UserControl = System.Windows.Controls.UserControl;

namespace WeSplitApp.View.DialogHelper
{
    /// <summary>
    /// Interaction logic for AddMemberDialog.xaml
    /// </summary>
    public partial class MemberAddDialog : UserControl
    {
        public MemberAddDialog()
        {
            InitializeComponent();
           // DataContext = new MemberAddDialogViewModel();
        }

        private void MemberAddAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                                 "All files (*.*)|*.*";
            fileDialog.Title = "My Image Browser";
            DialogResult dr = fileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string imagePath = fileDialog.FileName;

                var bitmap = new BitmapImage(
                    new Uri(imagePath, UriKind.Absolute)
                );
                MemberAvatar.Source = bitmap;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            int memberId = Convert.ToInt32((sender as Button).Tag);
            if(memberId != 0)
            {
                MEMBER reloadMember = HomeScreen.GetDatabaseEntities().MEMBERS.FirstOrDefault(item => item.MEMBER_ID == memberId);
                HomeScreen.GetDatabaseEntities().Entry(reloadMember).Reload();
                MemberListControl.updateMemberList();
            }
        }
    }
}
