using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    public class Slide3_TotalCostsViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;

                this.CurrentProceedsPieChartViewModel = new CurrentProceedsPieChartViewModel
                {
                    SelectedTrip = value
                };
            }
        }
        public CurrentProceedsPieChartViewModel CurrentProceedsPieChartViewModel { get; set; }

        public TRIP_MEMBER SelectedItem { get; set; }
        public SampleMessageDialogViewModel SampleMessageDialogViewModel { get; set; }
        public EditMemberDialogViewModel EditMemberDialogViewModel { get; set; }
        public AddExistingMemberDialogViewModel AddExistingMemberDialogViewModel { get; set; }
        public AddNewMemberAmountPaidToSelectedTripViewModel AddNewMemberAmountPaidToSelectedTripViewModel { get; set; }
        private ICommand _backToHomeScreenCommand { get; set; }
        public ICommand BackToHomeScreenCommand => this._backToHomeScreenCommand ?? (this._backToHomeScreenCommand = new CommandHandler((param) => BackToHomeScreenAction(), () => CanExecute));
        public ICommand RunMemberAddingDecisionCommand => new AnotherCommandImplementation(ExecuteMemberAddingDecisionDialog);
        private ICommand _editMemberCommand { get; set; }
        public ICommand EditMemberCommand => this._editMemberCommand ?? (this._editMemberCommand = new CommandHandler((param) => EditMemberAction(param), () => CanExecute));
        private ICommand _deleteMemberCommand { get; set; }
        public ICommand DeleteMemberCommand => this._deleteMemberCommand ?? (this._deleteMemberCommand = new CommandHandler((param) => DeleteMemberAction(param), () => CanExecute));
        public bool CanExecute => true;

        public static Slide3_TotalCostsViewModel Instance;

        public Slide3_TotalCostsViewModel()
        {
            Instance = this;
        }

        #region Execute dialog
        private async void EditMemberAction(object memberID)
        {
            int memberId = (int)memberID;
            this.SelectedItem = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(x => x.TRIP_ID == this.SelectedTrip.TRIP_ID && x.MEMBER_ID == memberId);


            ObservableCollection<MEMBER> ByMembers = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList());
            MEMBER removeMember = ByMembers.FirstOrDefault(item => item.MEMBER_ID == this.SelectedItem.MEMBER_ID);
            ByMembers.Remove(removeMember);

            this.EditMemberDialogViewModel = new EditMemberDialogViewModel
            {
                SelectedTripMember = this.SelectedItem,
                AvatarStatus = "CHANGE AVATAR",
                ByMembers = ByMembers,
                SelectedByMember = this.SelectedItem.MEMBER1
            };

            var view = new EditMemberDialog
            {
                DataContext = this.EditMemberDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, EditMemberClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private async void ExecuteMemberAddingDecisionDialog(object obj)
        {
            var view = new AddMemberToSelectedTripDesionDialog();

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, MemberAddingDecisionClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void MemberAddingDecisionClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter && parameter == false)
            {
                ExecuteAddNewMemberDialog();
            }
            else
            {
                ExecuteAddExistingMemberDialog();
            }
        }

        private async void ExecuteAddNewMemberDialog()
        {
            var total = this.SelectedTrip.TOTALCOSTS;
            var memberNumber = this.SelectedTrip.TRIP_MEMBER.Count + 1;
            var avarageMoney = total / memberNumber;

            this.EditMemberDialogViewModel = new EditMemberDialogViewModel
            {
                SelectedTripMember = new TRIP_MEMBER()
                {
                    AMOUNTPAID = avarageMoney,
                    MEMBER = new MEMBER()
                },
                AvatarStatus = "CHOOSE AVATAR",
                SelectedByMember = new MEMBER(),
                ByMembers = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList())
            };

            var view = new EditMemberDialog
            {
                DataContext = this.EditMemberDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, Slide3_ProceedsControl.DialogHostName, ExtendedOpenedEventHandler, AddNewMemberToTripClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void ExecuteAddExistingMemberDialog()
        {
            List<int> existsMemberID = (from member in this.SelectedTrip.TRIP_MEMBER
                                       select member.MEMBER_ID).ToList();
            var memberCollection = new ObservableCollection<MEMBER>(
                HomeScreen.GetDatabaseEntities().MEMBERS.ToList().Where(item => !existsMemberID.Contains(item.MEMBER_ID)));
            var tripMember = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.ToList();
            //var membersOfSelectedTrip = new ObservableCollection<MEMBER>(tripMember.Where(x => x.TRIP_ID == this.SelectedTrip.TRIP_ID).Select(x => x.MEMBER).ToList());

            this.AddExistingMemberDialogViewModel = new AddExistingMemberDialogViewModel
            {
                Members = memberCollection,
                ByMembers = new ObservableCollection<MEMBER>()
            };

            var view = new AddExistingMemberDialog
            {
                DataContext = this.AddExistingMemberDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, Slide3_ProceedsControl.DialogHostName, ExtendedOpenedEventHandler, AddNewMemberClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void DeleteMemberAction(object memberID) //Dialog hỏi xóa thành viên
        {
            int memberId = (int)memberID;
            this.SelectedItem = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(x => x.TRIP_ID == this.SelectedTrip.TRIP_ID && x.MEMBER_ID == memberId);

            this.SampleMessageDialogViewModel = new SampleMessageDialogViewModel
            {
                Message = "Xóa thành viên này?"
            };

            var view = new SampleMessageDialog
            {
                DataContext = this.SampleMessageDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, DeleteMemberClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private void DeleteMemberClosingEventHandler(object sender, DialogClosingEventArgs eventArgs) // xử lý khi xóa
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());

            DeleteTripMember(this.SelectedItem);
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        #endregion

        #region backend dialog
        public void DeleteTripMember(TRIP_MEMBER SelectedTripMember)
        {
            this.SelectedTrip.TRIP_MEMBER.Remove(SelectedTripMember);
            CaculateCurrentProcced();
            SaveNewTripToDB();
        }

        private void AddNewMemberToTripClosingEventHandler(object sender, DialogClosingEventArgs eventArgs) // Thêm mới thành viên
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());

            HomeScreen.GetDatabaseEntities().MEMBERS.Add(this.EditMemberDialogViewModel.SelectedTripMember.MEMBER);
            HomeScreen.GetDatabaseEntities().SaveChanges();
            this.EditMemberDialogViewModel.SelectedTripMember.TRIP_ID = this.SelectedTrip.TRIP_ID;
            HomeScreen.GetDatabaseEntities().TRIP_MEMBER.Add(this.EditMemberDialogViewModel.SelectedTripMember);
            HomeScreen.GetDatabaseEntities().SaveChanges();

            UpdatePieChar();
        }

        private void EditMemberClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!(eventArgs.Parameter is bool parameter) || parameter != false)
            {
                //OK, lets cancel the close...
                eventArgs.Cancel();

                //...now, lets update the "session" with some new content!
                eventArgs.Session.UpdateContent(new SampleProgressDialog());
                //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

                //lets run a fake operation for 3 seconds then close this baby.
                Task.Delay(TimeSpan.FromSeconds(3))
                    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                        TaskScheduler.FromCurrentSynchronizationContext());
                // tính toán lại tổng tiền thu

                CaculateCurrentProcced();
                SaveNewTripToDB();
                UpdatePieChar();
            }
            else
            {
                var reloadMember = HomeScreen.GetDatabaseEntities().MEMBERS.FirstOrDefault(item => item.MEMBER_ID == this.SelectedItem.MEMBER_ID);
                HomeScreen.GetDatabaseEntities().Entry(reloadMember).Reload();

                var reload = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(tripMember => tripMember.TRIP_ID == this.SelectedItem.TRIP_ID && tripMember.MEMBER_ID == this.SelectedItem.MEMBER_ID);
                HomeScreen.GetDatabaseEntities().Entry(reload).Reload();

                this.SelectedTrip.TRIP_MEMBER = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(item => item.TRIP_ID == this.SelectedTrip.TRIP_ID).TRIP_MEMBER;
            }
        }

        private void AddNewMemberClosingEventHandler(object sender, DialogClosingEventArgs eventArgs) // thêm thành viên có sẵn
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());

            foreach (var member in this.AddExistingMemberDialogViewModel.SelectedMembers)
            {
                var hasAlreadyInSelectedTrip = this.SelectedTrip.TRIP_MEMBER.Any(x => x.MEMBER_ID == member.MEMBER_ID);

                if (!hasAlreadyInSelectedTrip)
                {
                    if (member.ByMember_ID != null)
                        this.SelectedTrip.TRIP_MEMBER.Add(new TRIP_MEMBER
                        {
                            TRIP_ID = this.SelectedTrip.TRIP_ID,
                            MEMBER_ID = member.MEMBER_ID,
                            AMOUNTPAID = member.AmountPaid,
                            BYMEMBER_ID = member.ByMember_ID.MEMBER_ID
                        });
                    else
                        this.SelectedTrip.TRIP_MEMBER.Add(new TRIP_MEMBER
                        {
                            TRIP_ID = this.SelectedTrip.TRIP_ID,
                            MEMBER_ID = member.MEMBER_ID,
                            AMOUNTPAID = member.AmountPaid
                        });
                }
            }

            CaculateCurrentProcced();
            SaveNewTripToDB();
            UpdatePieChar();
        }

        #endregion

        #region support
        private void BackToHomeScreenAction()
        {
            HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
            HomeScreen.GetHomeScreenInstance().AddButton.Visibility = Visibility.Visible;
        }


        private void CaculateCurrentProcced()
        {
            double CurrentProcced = 0;
            foreach (var item in this.SelectedTrip.TRIP_MEMBER.ToList())
            {
                CurrentProcced = CurrentProcced + item.AMOUNTPAID;
            }

            this.SelectedTrip.CURRENTPROCEEDS = CurrentProcced;
        }

        private void SaveNewTripToDB()
        {
            var editTrip = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(item => item.TRIP_ID == this.SelectedTrip.TRIP_ID);
            editTrip = this.SelectedTrip;

            HomeScreen.GetDatabaseEntities().Entry(this.SelectedTrip).State = EntityState.Modified;
            HomeScreen.GetDatabaseEntities().SaveChanges();
        }

        private void UpdatePieChar()
        {
            this.CurrentProceedsPieChartViewModel = new CurrentProceedsPieChartViewModel
            {
                SelectedTrip = this.SelectedTrip
            };
        }
        #endregion
    }
}
