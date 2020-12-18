using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<MEMBER> ByMembers { get; set; }

        private MEMBER _selectedByMember;
        public MEMBER SelectedByMember
        {
            get => this._selectedByMember;
            set
            {
                this._selectedByMember = value;
                this.SelectedTripMember.MEMBER1 = value;
                if (value != null)
                {
                    this.SelectedTripMember.BYMEMBER_ID = this._selectedByMember.MEMBER_ID;
                }
                else
                {
                    this.SelectedTripMember.BYMEMBER_ID = null;
                }
            }
        }
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
