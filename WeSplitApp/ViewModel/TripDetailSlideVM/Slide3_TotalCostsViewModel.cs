using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View;

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
                //OnPropertyChanged();
                this.CurrentProceedsPieChartViewModel.SelectedTrip = this.SelectedTrip;
            }
        }
        public CurrentProceedsPieChartViewModel CurrentProceedsPieChartViewModel { get; set; }
        public bool IsEditing { get; set; }
        private ICommand _backToHomeScreenCommand { get; set; }
        public ICommand BackToHomeScreenCommand => this._backToHomeScreenCommand ?? (this._backToHomeScreenCommand = new CommandHandler(() => BackToHomeScreenAction(), () => CanExecute));
        private ICommand _editCommand { get; set; }
        public ICommand EditCommand => this._editCommand ?? (this._editCommand = new CommandHandler(() => EditAction(), () => CanExecute));
        
        public Slide3_TotalCostsViewModel()
        {
            this.CurrentProceedsPieChartViewModel = new CurrentProceedsPieChartViewModel();
        }
        private void BackToHomeScreenAction() => HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
        private void EditAction()
        {
            this.IsEditing = !this.IsEditing;
        }
        public bool CanExecute => true;
    }
}
