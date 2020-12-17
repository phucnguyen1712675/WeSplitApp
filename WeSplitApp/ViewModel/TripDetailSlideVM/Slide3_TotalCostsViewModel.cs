using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
        public bool IsEditing { get; set; }
        private ICommand _backToHomeScreenCommand { get; set; }
        public ICommand BackToHomeScreenCommand => this._backToHomeScreenCommand ?? (this._backToHomeScreenCommand = new CommandHandler((param) => BackToHomeScreenAction(), () => CanExecute));
        private ICommand _editCommand { get; set; }
        public ICommand EditCommand => this._editCommand ?? (this._editCommand = new CommandHandler((param) => EditAction(), () => CanExecute));
        public ICommand RunAddNewMemberCommand => new AnotherCommandImplementation(ExecuteAddNewMemberDialog);
        private ICommand _editMemberCommand { get; set; }
        public ICommand EditMemberCommand => this._editMemberCommand ?? (this._editMemberCommand = new CommandHandler((param) => EditMemberAction(param as TRIP_MEMBER), () => CanExecute));
        private ICommand _deleteMemberCommand { get; set; }
        public ICommand DeleteMemberCommand => this._deleteMemberCommand ?? (this._deleteMemberCommand = new CommandHandler((param) => DeleteMemberAction(), () => CanExecute));

        private async void DeleteMemberAction()
        {
            var view = new SampleMessageDialog
            {
                DataContext = new SampleMessageDialogViewModel
                {
                    Message = "Xóa thành viên này?"
                }
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
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void EditMemberAction(TRIP_MEMBER tRIP_MEMBER)
        {
            
        }

        private void ExecuteAddNewMemberDialog(object obj)
        {

        }
        private void BackToHomeScreenAction()
        {
            HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
            HomeScreen.GetHomeScreenInstance().AddButton.Visibility = Visibility.Visible;
        }
        private void EditAction()
        {
            this.IsEditing = !this.IsEditing;
        }
        public bool CanExecute => true;
    }
}
