using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
    public class Slide1_IntroViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;

                this.ImagePresenterViewModel = new ImagePresenterViewModel
                {
                    SelectedTrip = value
                };
                this.ToGoDatePickerViewModel = new CalenderPickerViewModel
                {
                    SelectedDate = (DateTime)this._selectedTrip.TOGODATE
                };
                this.ReturnDatePickerViewModel = new CalenderPickerViewModel
                {
                    SelectedDate = (DateTime)this._selectedTrip.RETURNDATE
                };
            }
        }
        public CalenderPickerViewModel ToGoDatePickerViewModel { get; set; }
        public CalenderPickerViewModel ReturnDatePickerViewModel { get; set; }
        public ImagePresenterViewModel ImagePresenterViewModel { get; set; }
        public bool IsTitleEditing { get; set; }
        public bool IsDescriptionEditing { get; set; }
        private ICommand _backToHomeScreenCommand { get; set; }
        public ICommand BackToHomeScreenCommand => this._backToHomeScreenCommand ?? (this._backToHomeScreenCommand = new CommandHandler((param) => BackToHomeScreenAction(), () => CanExecute));
        private ICommand _saveTitleCommand { get; set; }
        public ICommand SaveTitleCommand => this._saveTitleCommand ?? (this._saveTitleCommand = new CommandHandler((param) => SaveTitleAction(), () => CanExecute));
        private ICommand _saveDescriptionCommand { get; set; }
        public ICommand SaveDescriptionCommand => this._saveDescriptionCommand ?? (this._saveDescriptionCommand = new CommandHandler((param) => SaveDescriptionAction(), () => CanExecute));
        private ICommand _editTitleCommand { get; set; }
        public ICommand EditTitleCommand => this._editTitleCommand ?? (this._editTitleCommand = new CommandHandler((param) => EditTitleAction(), () => CanExecute));
        private ICommand _cancelTitleCommand { get; set; }
        public ICommand CancelTitleCommand => this._cancelTitleCommand ?? (this._cancelTitleCommand = new CommandHandler((param) => CancelTitleAction(), () => CanExecute));
        private ICommand _editDescriptionCommand { get; set; }
        public ICommand EditDescriptionCommand => this._editDescriptionCommand ?? (this._editDescriptionCommand = new CommandHandler((param) => EditDescriptionAction(), () => CanExecute));
        private ICommand _cancelDescriptionCommand { get; set; }
        public ICommand CancelDescriptionCommand => this._cancelDescriptionCommand ?? (this._cancelDescriptionCommand = new CommandHandler((param) => CancelDescriptionAction(), () => CanExecute));
        public ICommand RunToGoDatePickerDialogCommand => new AnotherCommandImplementation(ExecuteRunToGoDatePickerDialog);
        public ICommand RunReturnDatePickerDialogCommand => new AnotherCommandImplementation(ExecuteRunReturnDatePickerDialog);
        public ICommand RunImagePresenterDialogCommand => new AnotherCommandImplementation(ExecuteImagePresenterDialog);
        public ICommand RunAddNewImagesCommand => new AnotherCommandImplementation(ExecuteAddNewImageDialog);
        public Slide1_IntroViewModel()
        {
            this.IsTitleEditing = false;
            this.IsDescriptionEditing = false;
        }

        private TRIP _clonedTrip = new TRIP();
        private void BackToHomeScreenAction()
        {
            HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
            HomeScreen.GetHomeScreenInstance().AddButton.Visibility = Visibility.Visible;
        }
        private void ChangeEditingTitleVisibility() => this.IsTitleEditing = !this.IsTitleEditing;
        private void ChangeEditingDescriptionVisibility() => this.IsDescriptionEditing = !this.IsDescriptionEditing;
        private void SaveEditAction()
        {
            HomeScreen.GetDatabaseEntities().Entry(this.SelectedTrip).State = EntityState.Modified;
            HomeScreen.GetDatabaseEntities().SaveChanges();
        }
        private void EditTitleAction()
        {
            this._clonedTrip.TITTLE = this.SelectedTrip.TITTLE;

            ChangeEditingTitleVisibility();
        }
        private void CancelTitleAction()
        {
            this.SelectedTrip.TITTLE = this._clonedTrip.TITTLE;

            ChangeEditingTitleVisibility();
        }
        private void EditDescriptionAction()
        {
            this._clonedTrip.DESCRIPTION = this.SelectedTrip.DESCRIPTION;

            ChangeEditingDescriptionVisibility();
        }

        private void CancelDescriptionAction()
        {
            this.SelectedTrip.DESCRIPTION = this._clonedTrip.DESCRIPTION;

            ChangeEditingDescriptionVisibility();
        }
        private void SaveDescriptionAction()
        {
            SaveEditAction();

            ChangeEditingDescriptionVisibility();
        }

        private void SaveTitleAction()
        {
            SaveEditAction();

            ChangeEditingTitleVisibility();
        }
        public bool CanExecute => true;

        private async void ExecuteRunToGoDatePickerDialog(object obj)
        {
            var view = new CalenderPickerControl
            {
                DataContext = this.ToGoDatePickerViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, ToGoDatePickerClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private async void ExecuteRunReturnDatePickerDialog(object obj)
        {
            var view = new CalenderPickerControl
            {
                DataContext = this.ReturnDatePickerViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, ReturnDatePickerClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private async void ExecuteImagePresenterDialog(object obj)
        {
            var view = new ImagePresenterControl
            {
                DataContext = this.ImagePresenterViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, ImagePresenterClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private async void ExecuteAddNewImageDialog(object obj)
        {
            OpenMultipleFilesDialogArguments dialogArgs = new OpenMultipleFilesDialogArguments()
            {
                Width = 600,
                Height = 600,
                Filters = "All files|*.*|C# files|*.cs|XAML files|*.xaml"
            };

            OpenMultipleFilesDialogResult result = await OpenMultipleFilesDialog.ShowDialogAsync(HomeScreen.DialogHostName, dialogArgs);

            if (!result.Canceled)
            {
                result.Files.ForEach(file => {

                    if (file.Contains(AppDomain.CurrentDomain.BaseDirectory))
                    {
                        file = "\\" + file.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length);
                    }
                    var newTripImages = new TRIP_IMAGES
                    {
                        TRIP_ID = this.SelectedTrip.TRIP_ID,
                        IMAGE = file
                    };
                    //this.SelectedTrip.TRIP_IMAGES.Add(newTripImages);
                    HomeScreen.GetDatabaseEntities().TRIP_IMAGES.Add(newTripImages);
                });
                HomeScreen.GetDatabaseEntities().SaveChanges();
                var temp = HomeScreen.GetDatabaseEntities().TRIPS.FirstOrDefault(trip => trip.TRIP_ID == this.SelectedTrip.TRIP_ID);
                this.ImagePresenterViewModel = new ImagePresenterViewModel
                {
                    SelectedTrip = temp
                };
            }
        }
        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void ToGoDatePickerClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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

            var isValid = this.ToGoDatePickerViewModel.SelectedDate <= this.SelectedTrip.RETURNDATE;

            if (isValid)
            {
                this._selectedTrip.TOGODATE = this.ToGoDatePickerViewModel.SelectedDate;
                this.SaveEditAction();
                this.ToGoDatePickerViewModel = new CalenderPickerViewModel
                {
                    SelectedDate = (DateTime)this._selectedTrip.TOGODATE
                };
            }
            else
            {
                MessageBox.Show("Ngày đi không hợp lệ");
            }
        }
        private void ReturnDatePickerClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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

            var isValid = this.ReturnDatePickerViewModel.SelectedDate >= this.SelectedTrip.TOGODATE;

            if (isValid)
            {
                this._selectedTrip.RETURNDATE = this.ReturnDatePickerViewModel.SelectedDate;
                this.SaveEditAction();
                this.ReturnDatePickerViewModel = new CalenderPickerViewModel
                {
                    SelectedDate = (DateTime)this._selectedTrip.RETURNDATE
                };
            }
            else
            {
                MessageBox.Show("Ngày về không hợp lệ");
            }
        }
        private void ImagePresenterClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;
        }
    }
}
