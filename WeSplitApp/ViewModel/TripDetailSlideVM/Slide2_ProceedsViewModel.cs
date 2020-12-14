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
    public class Slide2_ProceedsViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;
                //OnPropertyChanged();
                this.TotalCostsPieChartViewModel.SelectedTrip = this.SelectedTrip;
            }
        }
        public TotalCostsPieChartViewModel TotalCostsPieChartViewModel { get; set; }
        public bool IsEditing { get; set; }
        private ICommand _backToHomeScreenCommand { get; set; }
        public ICommand BackToHomeScreenCommand => this._backToHomeScreenCommand ?? (this._backToHomeScreenCommand = new CommandHandler(() => BackToHomeScreenAction(), () => CanExecute));
        private ICommand _editCommand { get; set; }
        public ICommand EditCommand => this._editCommand ?? (this._editCommand = new CommandHandler(() => EditAction(), () => CanExecute));

        public Slide2_ProceedsViewModel()
        {
            this.TotalCostsPieChartViewModel = new TotalCostsPieChartViewModel();
        }
        private void BackToHomeScreenAction() => HomeScreen.SetNewContentControl(new TripsCollectionViewModel());
        private void EditAction()
        {
            this.IsEditing = !this.IsEditing;
        }
        public bool CanExecute => true;

    }
}
