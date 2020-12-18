using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WeSplitApp.Utils;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class EditMemberDialogViewModel : ViewModel
    {
        public TRIP_MEMBER SelectedTripMember { get; set; }
        public string AvatarStatus { get; set; }
        public ICommand ChangeAvatarCommand => new AnotherCommandImplementation(ExecuteChangeAvatarDialog);
        private void ExecuteChangeAvatarDialog(object obj)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                                 "All files (*.*)|*.*",
                Title = "Chọn ảnh đại diện mới"
            };
            DialogResult dr = fileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string imagePath = fileDialog.FileName;

                this.SelectedTripMember.MEMBER.AVATAR = imagePath;
            }
        }
    }
}
