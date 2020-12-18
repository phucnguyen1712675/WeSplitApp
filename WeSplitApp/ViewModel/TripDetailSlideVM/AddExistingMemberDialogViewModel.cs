using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;
using WeSplitApp.View.Controls.TripDetailSlide;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public class AddExistingMemberDialogViewModel : ViewModel
    {
        private ObservableCollection<MEMBER> _members;
        public ObservableCollection<MEMBER> Members
        {
            get => this._members;
            set
            {
                this._members = value;
                GetMembersToShow();
            }
        }

        private ObservableCollection<MEMBER> _byMembers;
        public ObservableCollection<MEMBER> ByMembers
        {
            get => this._byMembers;
            set
            {
                this._byMembers = value;
                AddExistingMemberDialog.updateByMemberListComboBox();
            }
        }

        private int NewMemberCount;
        public ObservableCollection<MemberChipStyle> SelectedMembers { get; set; }
        public ObservableCollection<MemberChipStyle> MEMBERSTOSHOW { get; set; }
        public MemberChipStyle SelectedMember { get; set; }
        public MEMBER selectedByMember { get; set; }
        public Visibility InputFormVisibility { get; set; }
        public Visibility ImageBackgroundVisibility => this.InputFormVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        public bool AutoAddAmountPaid { get; set; }
        private ICommand _runCheckedCommand { get; set; }
        public ICommand RunCheckedCommand => this._runCheckedCommand ?? (this._runCheckedCommand = new CommandHandler((param) => ExecuteRunCheckedAction(param), () => CanExecute));
        private ICommand _acceptCommand { get; set; }
        public ICommand AcceptCommand => this._acceptCommand ?? (this._acceptCommand = new CommandHandler((param) => ExecuteAcceptAction(), () => CanExecute));
        private ICommand _cancelCommand { get; set; }
        public ICommand CancelCommand => this._cancelCommand ?? (this._cancelCommand = new CommandHandler((param) => ExecuteCancelAction(), () => CanExecute));
        private ICommand _changeAutoAddCommand { get; set; }
        public ICommand ChangeAutoAddCommand => this._changeAutoAddCommand ?? (this._changeAutoAddCommand = new CommandHandler((param) => ExecuteChangeAutoAddAction(), () => CanExecute));

        public bool CanExecute => true;

        public AddExistingMemberDialogViewModel()
        {
            this.InputFormVisibility = Visibility.Collapsed;
            this.AutoAddAmountPaid = true;
            this.NewMemberCount = 0;
        }
        private void ExecuteChangeAutoAddAction() => this.AutoAddAmountPaid = !this.AutoAddAmountPaid;
        private void ChangeInputFormVisibility() => this.InputFormVisibility = this.InputFormVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        private void ExecuteCancelAction()
        {
            this.SelectedMember.IsChecked = false;
            ChangeInputFormVisibility();
        }
        private void ExecuteAcceptAction() //khi bấm thêm dô list temp
        {
            SelectedMembers.Add(this.SelectedMember);
            if (this.AutoAddAmountPaid)
            {
                for (int i = 0; i < this.NewMemberCount; i++)
                {
                    this.SelectedMembers[i].AmountPaid = this.SelectedMember.AmountPaid;
                }
            }
            this.NewMemberCount++;
            ChangeInputFormVisibility();
        }

        private void GetMembersToShow()
        {
            this.SelectedMembers = new ObservableCollection<MemberChipStyle>();
            this.MEMBERSTOSHOW = new ObservableCollection<MemberChipStyle>();

            foreach (var member in this.Members)
            {
                var isChecked = Slide3_TotalCostsViewModel.Instance.SelectedTrip.TRIP_MEMBER.Any(x => x.MEMBER_ID == member.MEMBER_ID);

                MEMBER ByMember_ID = new MEMBER();

                var newMember = new MemberChipStyle
                {
                    MEMBER_ID = member.MEMBER_ID,
                    NAME = member.NAME,
                    ByMember_ID = ByMember_ID,
                    PHONENUMBER = member.PHONENUMBER,
                    AVATAR = member.AVATAR,
                    GENDER = member.GENDER,
                    IsChecked = isChecked,
                    AmountPaid = 0.0
                };

                if (isChecked)
                {
                    this.SelectedMembers.Add(newMember);
                }
                this.MEMBERSTOSHOW.Add(newMember);
            }
        }
        private void ExecuteRunCheckedAction(object memberId) // khi bấm toggle button
        {
            var memberID = (int)memberId;
            if (memberID != 0)
            {
                var member = this.MEMBERSTOSHOW.FirstOrDefault(x => x.MEMBER_ID == memberID);

                if (member.IsChecked) // khi bấm Thêm vào, hiện bảng điền thông tin
                {
                    if (this.SelectedMember != null && member.MEMBER_ID != this.SelectedMember.MEMBER_ID) // check có đang điền 1 thằng dở dang ko
                    {
                        var isNotInSelectedMemberList = !this.SelectedMembers.Any(x => x.MEMBER_ID == this.SelectedMember.MEMBER_ID);

                        if (isNotInSelectedMemberList)
                        {
                            ExecuteCancelAction();
                        }
                    }

                    //mở bảng nhập tiền thong tin
                    this.SelectedMember = member;
                    this.ByMembers = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList());
                    MEMBER removeMember = ByMembers.FirstOrDefault(item => item.MEMBER_ID == this.SelectedMember.MEMBER_ID);
                    this.ByMembers.Remove(removeMember);
                    int totalMember = Slide3_TotalCostsViewModel.Instance.SelectedTrip.TRIP_MEMBER.Count + this.SelectedMembers.Count + 1;
                    var avarageMoney = Slide3_TotalCostsViewModel.Instance.SelectedTrip.TOTALCOSTS / totalMember;
                    this.SelectedMember.AmountPaid = avarageMoney;

                    ChangeInputFormVisibility();
                }
                else // khi bấm xóa
                {
                    if (member.MEMBER_ID != this.SelectedMember.MEMBER_ID)
                    {
                        SelectedMembers.Remove(member);
                    }
                    else
                    {
                        ExecuteCancelAction();
                    }
                }
            }
        }
    }
    public class MemberChipStyle : MEMBER
    {
        public bool IsChecked { get; set; }
        public double AmountPaid { get; set; }
        public MEMBER ByMember_ID { get; set; }
    }
}
