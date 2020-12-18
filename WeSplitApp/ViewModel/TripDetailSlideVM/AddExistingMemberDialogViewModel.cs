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
        private int OldMemberCount;
        private int NewMemberCount;
        public ObservableCollection<MemberChipStyle> SelectedMembers { get; set; }
        public ObservableCollection<MemberChipStyle> MEMBERSTOSHOW { get; set; }
        public MemberChipStyle SelectedMember { get; set; }
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
            this.OldMemberCount = 0;
            this.NewMemberCount = 0;
        }
        private void ExecuteChangeAutoAddAction() => this.AutoAddAmountPaid = !this.AutoAddAmountPaid;
        private void ChangeInputFormVisibility() => this.InputFormVisibility = this.InputFormVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        private void ExecuteCancelAction()
        {
            this.SelectedMember.IsChecked = false;
            ChangeInputFormVisibility();
        }
        private void ExecuteAcceptAction()
        {
            if (this.AutoAddAmountPaid)
            {
                for (int i = 0; i < this.NewMemberCount; i++)
                {
                    this.SelectedMembers[this.OldMemberCount + i].AmountPaid = this.SelectedMember.AmountPaid;
                }
            }
            SelectedMembers.Add(this.SelectedMember);
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
                
                var amountPaid = isChecked ? HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(x => x.MEMBER_ID == member.MEMBER_ID && x.TRIP_ID == Slide3_TotalCostsViewModel.Instance.SelectedTrip.TRIP_ID).AMOUNTPAID : 0.0; 

                var newMember = new MemberChipStyle
                {
                    MEMBER_ID = member.MEMBER_ID,
                    NAME = member.NAME,
                    PHONENUMBER = member.PHONENUMBER,
                    AVATAR = member.AVATAR,
                    GENDER = member.GENDER,
                    IsChecked = isChecked,
                    AmountPaid = amountPaid
                };

                if (isChecked)
                {
                    this.SelectedMembers.Add(newMember);
                    this.OldMemberCount++;
                }
                this.MEMBERSTOSHOW.Add(newMember);
            }
        }
        private void ExecuteRunCheckedAction(object memberId)
        {
            var memberID = (int)memberId;
            if (memberID != 0)
            {
                var member = this.MEMBERSTOSHOW.FirstOrDefault(x => x.MEMBER_ID == memberID);



                if (member.IsChecked)
                {
                    if (this.SelectedMember != null && member.MEMBER_ID != this.SelectedMember.MEMBER_ID)
                    {
                        var isNotInSelectedMemberList = !this.SelectedMembers.Any(x => x.MEMBER_ID == this.SelectedMember.MEMBER_ID);

                        if (isNotInSelectedMemberList)
                        {
                            ExecuteCancelAction();
                        }
                    }
                    this.SelectedMember = member;

                    var avarageMoney = Slide3_TotalCostsViewModel.Instance.SelectedTrip.TOTALCOSTS / (this.SelectedMembers.Count + 1);
                    this.SelectedMember.AmountPaid = avarageMoney;
                    
                    ChangeInputFormVisibility();
                }
                else
                {
                    if (member.MEMBER_ID != this.SelectedMember.MEMBER_ID)
                    {
                        var isOldMember = Slide3_TotalCostsViewModel.Instance.SelectedTrip.TRIP_MEMBER.Any(x => x.MEMBER_ID == member.MEMBER_ID);

                        if (isOldMember)
                        {
                            var messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.OKCancel);

                            if (messageBoxResult == MessageBoxResult.OK)
                            {
                                SelectedMembers.Remove(member);
                                var TripMember = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(x => x.MEMBER_ID == this.SelectedMember.MEMBER_ID);
                                Slide3_TotalCostsViewModel.Instance.DeleteTripMember(TripMember);
                            }
                            else
                            {
                                member.IsChecked = true;
                            }
                        }
                        else
                        {
                            SelectedMembers.Remove(member);
                        }
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
    }
}
