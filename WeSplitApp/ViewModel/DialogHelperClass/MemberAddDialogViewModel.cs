using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel.DialogHelperClass
{
    class MemberAddDialogViewModel : AddDialogBaseViewModel
    {
        public MEMBER NewMember { get; set; }
        public string Tittle { get; set; }
        public MemberAddDialogViewModel(MEMBER member, string tittle) { 
            NewMember = member;
            Tittle = tittle;
            if (member.MEMBER_ID == 0) type = "add";
            else type = "edit";
        }

        public override void YesCommandAction()
        {
            if (!string.IsNullOrEmpty(NewMember.NAME)
                && !string.IsNullOrEmpty(NewMember.PHONENUMBER)
                && !string.IsNullOrEmpty(NewMember.AVATAR))
            {
                if (NewMember.GENDER == null) NewMember.GENDER = false;

                if(type == "add")
                {
                    HomeScreen.GetDatabaseEntities().MEMBERS.Add(NewMember);

                    // trường hợp đang làm trong màn hình AddTrip <=> refresh list Members
                    if(AddNewTripViewModel.Instance != null) AddNewTripViewModel.Instance.MEMBERs.Add(NewMember);
                    //trường hợp đang làm trong màn hình MemberList <=> refresh list Members
                    MemberListViewModel.updateList(NewMember);
                }
                else
                {
                    MEMBER member = HomeScreen.GetDatabaseEntities().MEMBERS.First(item => item.MEMBER_ID == NewMember.MEMBER_ID);
                    member = NewMember;
                }
                HomeScreen.GetDatabaseEntities().SaveChanges();
                MessageBox.Show("Thành công");
            }
            else
            {
                MessageBox.Show("Điền thiếu dữ liệu !");
            }
        }
    }
}
