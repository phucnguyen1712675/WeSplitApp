using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplitApp.Utils;

namespace WeSplitApp.ViewModel
{
    public class CurrentProceedsPieChartViewModel : ViewModel
    {
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;
                //OnPropertyChanged();
                GetTripMemberPaidAmount();
            }
        }
        public SeriesCollection CurrentProceedsPieChart { get; set; }
        public LegendLocation LegendLocation { get; set; }
        public string ActionDescribe { get; set; }
        private ICommand _showDetailCommand { get; set; }
        public ICommand ShowDetailCommand => this._showDetailCommand ?? (this._showDetailCommand = new CommandHandler(() => MyShowDetailAction(), () => CanExecute));
        
        public CurrentProceedsPieChartViewModel()
        {
            this.LegendLocation = LegendLocation.None;
            this.ActionDescribe = "SHOW DETAIL";
            this.CurrentProceedsPieChart = new SeriesCollection();
        }
        private void GetTripMemberPaidAmount()
        {
            foreach (var item in this.SelectedTrip.TRIP_MEMBER)
            {
                this.CurrentProceedsPieChart.Add(new PieSeries
                {
                    Title = item.MEMBER.NAME,
                    Values = new ChartValues<double> { item.AMOUNTPAID }
                });
            }
        }
        public bool CanExecute => true;
        public void MyShowDetailAction()
        {
            this.LegendLocation = this.LegendLocation == LegendLocation.None ? LegendLocation.Top : LegendLocation.None;
            this.ActionDescribe = this.ActionDescribe.Equals("SHOW DETAIL") ? "CLOSE DETAIL" : "SHOW DETAIL";
        }
    }
}
