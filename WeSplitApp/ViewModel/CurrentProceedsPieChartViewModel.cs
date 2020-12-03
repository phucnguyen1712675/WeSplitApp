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
        private SeriesCollection _currentProceedsPieChart;
        public SeriesCollection CurrentProceedsPieChart
        {
            get => this._currentProceedsPieChart;
            set
            {
                this._currentProceedsPieChart = value;
                OnPropertyChanged();
            }
        }

        private LegendLocation _legendLocation;
        public LegendLocation LegendLocation
        {
            get => this._legendLocation;
            set
            {
                this._legendLocation = value;
                OnPropertyChanged();
            }
        }

        private string _actionDescribe;
        public string ActionDescribe
        {
            get => this._actionDescribe;
            set
            {
                this._actionDescribe = value;
                OnPropertyChanged();
            }
        }
        private ICommand _showDetailCommand { get; set; }
        public ICommand ShowDetailCommand => this._showDetailCommand ?? (this._showDetailCommand = new CommandHandler(() => MyShowDetailAction(), () => CanExecute));
        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MyShowDetailAction()
        {
            this.LegendLocation = this.LegendLocation == LegendLocation.None ? LegendLocation.Top : LegendLocation.None;
            this.ActionDescribe = this.ActionDescribe.Equals("SHOW DETAIL") ? "CLOSE DETAIL" : "SHOW DETAIL";
        }
        public CurrentProceedsPieChartViewModel(TRIP trip)
        {
            this.LegendLocation = LegendLocation.None;
            this.ActionDescribe = "SHOW DETAIL";
            this.CurrentProceedsPieChart = new SeriesCollection();

            GetTripMemberPaidAmount(trip);
        }

        private void GetTripMemberPaidAmount(TRIP trip)
        {
            foreach (var item in trip.TRIP_MEMBER)
            {
                this.CurrentProceedsPieChart.Add(new PieSeries
                {
                    Title = item.MEMBER.NAME,
                    Values = new ChartValues<double> { item.AMOUNTPAID }
                });
            }
        }
    }
}
