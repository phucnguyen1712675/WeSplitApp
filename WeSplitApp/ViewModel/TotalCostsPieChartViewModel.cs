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
        private TRIP _selectedTrip;
        public TRIP SelectedTrip
        {
            get => this._selectedTrip;
            set
            {
                this._selectedTrip = value;
                //OnPropertyChanged();
                GetTripLocationCosts();
                GetTripCostIncurredCost();
            }
        }
        public SeriesCollection TotalCostsPieChart { get; set; }
        public LegendLocation LegendLocation { get; set; }
        public string ActionDescribe { get; set; }
        private ICommand _showDetailCommand { get; set; }
        public ICommand ShowDetailCommand => this._showDetailCommand ?? (this._showDetailCommand = new CommandHandler(() => MyShowDetailAction(), () => CanExecute));
        
        public TotalCostsPieChartViewModel()
        {
            this.LegendLocation = LegendLocation.None;
            this.ActionDescribe = "SHOW DETAIL";
            this.TotalCostsPieChart = new SeriesCollection();
        }
        private void GetTripLocationCosts()
        {
            foreach (var item in this.SelectedTrip.TRIP_LOCATION)
            {
                this.TotalCostsPieChart.Add(new PieSeries
                {
                    Title = item.LOCATION.NAME,
                    Values = new ChartValues<double> { item.COSTS },
                });
            }
        }
        private void GetTripCostIncurredCost()
        {
            foreach (var item in this.SelectedTrip.TRIP_COSTINCURRED)
            {
                this.TotalCostsPieChart.Add(new PieSeries
                {
                    Title = item.COSTINCURRED.NAME,
                    Values = new ChartValues<double> { Convert.ToDouble(item.COST) },
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
