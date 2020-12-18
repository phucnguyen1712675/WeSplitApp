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

        //private TRIP_MEMBER tempTripMember;
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

        public static Slide3_TotalCostsViewModel Instance;

        public Slide3_TotalCostsViewModel()
        {
            Instance = this;
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

        private async void DeleteMemberAction(object memberID)
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

        private void DeleteMemberClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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

        public void DeleteTripMember(TRIP_MEMBER SelectedTripMember)
        {
            HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(trip => trip.TRIP_ID == this.SelectedTrip.TRIP_ID).TRIP_MEMBER.Remove(SelectedTripMember);

            HomeScreen.GetDatabaseEntities().SaveChanges();

            var temp = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(trip => trip.TRIP_ID == this.SelectedTrip.TRIP_ID);

            this.CurrentProceedsPieChartViewModel = new CurrentProceedsPieChartViewModel
            {
                SelectedTrip = temp
            };
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private async void EditMemberAction(object memberID)
        {
            int memberId = (int)memberID;
            this.SelectedItem = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(x => x.TRIP_ID == this.SelectedTrip.TRIP_ID && x.MEMBER_ID == memberId);

            this.EditMemberDialogViewModel = new EditMemberDialogViewModel
            {
                SelectedTripMember = this.SelectedItem,
                AvatarStatus = "CHANGE AVATAR"
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
        private async void ExecuteAddNewMemberDialog()
        {
            var total = this.SelectedTrip.TOTALCOSTS;
            var memberNumber = this.SelectedTrip.TRIP_MEMBER.Count + 1;
            var avarageMoney = total / memberNumber;

            this.EditMemberDialogViewModel = new EditMemberDialogViewModel
            {
                SelectedTripMember = new TRIP_MEMBER
                {
                    AMOUNTPAID = avarageMoney
                },
                AvatarStatus = "CHOOSE AVATAR"
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
            var memberCollection = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList());
            var tripMember = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.ToList();
            //var membersOfSelectedTrip = new ObservableCollection<MEMBER>(tripMember.Where(x => x.TRIP_ID == this.SelectedTrip.TRIP_ID).Select(x => x.MEMBER).ToList());

            this.AddExistingMemberDialogViewModel = new AddExistingMemberDialogViewModel
            {
                Members = memberCollection,
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
        private void AddNewMemberToTripClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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
            HomeScreen.GetDatabaseEntities().TRIP_MEMBER.Add(this.EditMemberDialogViewModel.SelectedTripMember);
            HomeScreen.GetDatabaseEntities().SaveChanges();

            var temp = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(trip => trip.TRIP_ID == this.SelectedTrip.TRIP_ID);

            this.CurrentProceedsPieChartViewModel = new CurrentProceedsPieChartViewModel
            {
                SelectedTrip = temp
            };
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

                HomeScreen.GetDatabaseEntities().Entry(this.SelectedTrip).State = EntityState.Modified;
                HomeScreen.GetDatabaseEntities().SaveChanges();
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

        private void AddNewMemberClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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
                    HomeScreen.GetDatabaseEntities().TRIP_MEMBER.Add(new TRIP_MEMBER
                    {
                        TRIP_ID = this.SelectedTrip.TRIP_ID,
                        MEMBER_ID = member.MEMBER_ID,
                        AMOUNTPAID = member.AmountPaid
                    });
                }
            }
            HomeScreen.GetDatabaseEntities().SaveChanges();

            /*var reload = HomeScreen.GetDatabaseEntities().TRIP_MEMBER.FirstOrDefault(tripMember => tripMember.TRIP_ID == this.SelectedTrip.TRIP_ID);
            HomeScreen.GetDatabaseEntities().Entry(reload).Reload();*/

            //this.SelectedTrip.TRIP_MEMBER = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(item => item.TRIP_ID == this.SelectedTrip.TRIP_ID).TRIP_MEMBER;
            

            var tempTrip = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(item => item.TRIP_ID == this.SelectedTrip.TRIP_ID);
            HomeScreen.GetDatabaseEntities().Entry(tempTrip).Reload();

            this.SelectedTrip = tempTrip;
        }
        private void BackToHomeScreenAction() => HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
        public bool CanExecute => true;
    }
}
