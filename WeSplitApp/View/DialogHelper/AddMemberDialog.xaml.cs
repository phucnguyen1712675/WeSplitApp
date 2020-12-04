using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplitApp.View.Controls;
using WeSplitApp.ViewModel.DialogHelperClass;
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
                var folder = AppDomain.CurrentDomain.BaseDirectory;

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
                MEMBER reloadMember = HomeScreen.homeScreen.database.MEMBERS.FirstOrDefault(item => item.MEMBER_ID == memberId);
                HomeScreen.homeScreen.database.Entry(reloadMember).Reload();
                MemberListControl.updateUI();
            }
        }
    }
}
