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
    public class Slide2_ProceedsViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;

                this.TotalCostsPieChartViewModel = new TotalCostsPieChartViewModel
                {
                    SelectedTrip = value
                };
            }
        }
        public TotalCostsPieChartViewModel TotalCostsPieChartViewModel { get; set; }
        public AddLocationCostDialogViewModel AddLocationCostDialogViewModel { get; set; }
        public AddOtherCostDialogViewModel AddOtherCostDialogViewModel { get; set; }
        private ICommand _backToHomeScreenCommand { get; set; }
        public ICommand BackToHomeScreenCommand => this._backToHomeScreenCommand ?? (this._backToHomeScreenCommand = new CommandHandler((param) => BackToHomeScreenAction(), () => CanExecute));
        public ICommand RunAddLocationCostCommand => new AnotherCommandImplementation(ExecuteAddLocationCostDialog);
        public ICommand RunAddOtherCostCommand => new AnotherCommandImplementation(ExecuteAddOtherCostDialog);

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private async void ExecuteAddOtherCostDialog(object obj)
        {
            var db = HomeScreen.GetDatabaseEntities();
            var trip = db.TRIPS.Find(this.SelectedTrip.TRIP_ID);
            var removeCostIncurreds = trip.TRIP_COSTINCURRED.ToList();
            var costIncurredList = db.COSTINCURREDS.ToList();
            var costIncurredCollection = new ObservableCollection<COSTINCURRED>(costIncurredList);

            removeCostIncurreds.ForEach(x =>
            {
                costIncurredCollection.Remove(x.COSTINCURRED);
            });

            var isEnabledSuggestion = costIncurredCollection.Count != 0;
            var selectedIndexListBox = isEnabledSuggestion ? 0 : 1;

            this.AddOtherCostDialogViewModel = new AddOtherCostDialogViewModel
            {
                CostIncurredList = costIncurredCollection,
                IsEnabledSuggestion = isEnabledSuggestion,
                SelectedIndexListBox = selectedIndexListBox
            };

            var view = new AddOtherCostDialog
            {
                DataContext = this.AddOtherCostDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, AddOtherCostDialogClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async void ExecuteAddLocationCostDialog(object obj)
        {
            var db = HomeScreen.GetDatabaseEntities();
            var trip = db.TRIPS.Find(this.SelectedTrip.TRIP_ID);
            var removeLocations = trip.TRIP_LOCATION.ToList();
            var locationList = db.LOCATIONS.ToList();
            var locationCollection = new ObservableCollection<LOCATION>(locationList);

            removeLocations.ForEach(x =>
            {
                locationCollection.Remove(x.LOCATION);
            });

            this.AddLocationCostDialogViewModel = new AddLocationCostDialogViewModel
            {
                LocationList = locationCollection
            };

            var view = new AddLocationCostDialog
            {
                DataContext = this.AddLocationCostDialogViewModel
            };

            //show the dialog
            var result = await DialogHost.Show(view, HomeScreen.DialogHostName, ExtendedOpenedEventHandler, AddLocationCostDialogClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private void AddOtherCostDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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

            var db = HomeScreen.GetDatabaseEntities();
            var trip = db.TRIPS.Find(this.SelectedTrip.TRIP_ID);
            var selectedIndexListBox = this.AddOtherCostDialogViewModel.SelectedIndexListBox;
            var cost = this.AddOtherCostDialogViewModel.Cost;

            if (cost != 0.0)
            {
                var newCostIncurredId = 0;
                COSTINCURRED newCostIncurred;

                if (selectedIndexListBox == 0)
                {
                    newCostIncurred = this.AddOtherCostDialogViewModel.SelectedItem;
                }
                else
                {
                    newCostIncurred = this.AddOtherCostDialogViewModel.NewCostIncurred;
                    db.COSTINCURREDS.Add(newCostIncurred);
                    db.SaveChanges();
                }

                newCostIncurredId = newCostIncurred.COST_ID;
                if (newCostIncurredId == 0)
                {
                    MessageBox.Show("Lỗi!");
                    return;
                }

                var newTripCostIncurred = new TRIP_COSTINCURRED
                {
                    TRIP_ID = trip.TRIP_ID,
                    COST_ID = newCostIncurredId,
                    COST = cost
                };
                trip.TRIP_COSTINCURRED.Add(newTripCostIncurred);
                trip.TOTALCOSTS = CalculateTotalCosts();
                db.SaveChanges();

                this.TotalCostsPieChartViewModel = new TotalCostsPieChartViewModel
                {
                    SelectedTrip = trip
                };
            }
            else
            {
                MessageBox.Show("Nhập không hợp lệ!");
            }
        }

        private void AddLocationCostDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
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

            var db = HomeScreen.GetDatabaseEntities();
            var trip = db.TRIPS.Find(this.SelectedTrip.TRIP_ID);
            var selectedIndexListBox = this.AddLocationCostDialogViewModel.SelectedIndexListBox;
            var cost = this.AddLocationCostDialogViewModel.Cost;

            if (cost != 0.0)
            {
                var newLocationId = 0;
                LOCATION newLocation;

                if (selectedIndexListBox == 0)
                {
                    newLocation = this.AddLocationCostDialogViewModel.SelectedItem;
                }
                else
                {
                    newLocation = this.AddLocationCostDialogViewModel.NewLocation;
                    db.LOCATIONS.Add(newLocation);
                    db.SaveChanges();
                }

                newLocationId = newLocation.LOCATION_ID;
                if (newLocationId == 0)
                {
                    MessageBox.Show("Lỗi!");
                    return;
                }

                var newTripLocation = new TRIP_LOCATION
                {
                    TRIP_ID = trip.TRIP_ID,
                    LOCATION_ID = newLocationId,
                    COSTS = cost
                };

                trip.TRIP_LOCATION.Add(newTripLocation);
                trip.TOTALCOSTS = CalculateTotalCosts();
                db.SaveChanges();

                this.TotalCostsPieChartViewModel = new TotalCostsPieChartViewModel
                {
                    SelectedTrip = trip
                };
            }
            else
            {
                MessageBox.Show("Nhập không hợp lệ!");
            }
        }
        private double CalculateTotalCosts()
        {
            var db = HomeScreen.GetDatabaseEntities();
            var trip = db.TRIPS.Find(this.SelectedTrip.TRIP_ID);
            var tripLocation = trip.TRIP_LOCATION.ToList();
            var tripCostIncurred = trip.TRIP_COSTINCURRED.ToList();

            var total = 0.0;

            tripLocation.ForEach(x =>
            {
                total += x.COSTS;
            });

            tripCostIncurred.ForEach(x =>
            {
                total += (double)x.COST;
            });

            return total;
        }
        private void BackToHomeScreenAction()
        {
            HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
            HomeScreen.GetHomeScreenInstance().AddButton.Visibility = Visibility.Visible;
        }
        public bool CanExecute => true;
    }
}
