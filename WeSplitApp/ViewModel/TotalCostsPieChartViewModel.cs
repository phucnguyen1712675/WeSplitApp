using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplitApp.Utils;
using WeSplitApp.View.Controls;

namespace WeSplitApp.ViewModel
{
    public class TotalCostsPieChartViewModel : ViewModel
    {
        private SeriesCollection _totalCostsPieChart;
        public SeriesCollection TotalCostsPieChart
        {
            get => this._totalCostsPieChart;
            set
            {
                this._totalCostsPieChart = value;
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
        public TotalCostsPieChartViewModel(TRIP trip)
        {
            this.LegendLocation = LegendLocation.None;
            this.ActionDescribe = "SHOW DETAIL";
            this.TotalCostsPieChart = new SeriesCollection();

            GetTripLocationCosts(trip);
            GetTripCostIncurredCost(trip);
        }

        private void GetTripLocationCosts(TRIP trip)
        {
            foreach (var item in trip.TRIP_LOCATION)
            {
                this.TotalCostsPieChart.Add(new PieSeries
                {
                    Title = item.LOCATION.NAME,
                    Values = new ChartValues<double> { item.COSTS }
                });
            }
        }

        private void GetTripCostIncurredCost(TRIP trip)
        {
            foreach (var item in trip.TRIP_COSTINCURRED)
            {
                this.TotalCostsPieChart.Add(new PieSeries
                {
                    Title = item.COSTINCURRED.NAME,
                    Values = new ChartValues<double> { Convert.ToDouble(item.COST) }
                });
            }
        }
    }
}
