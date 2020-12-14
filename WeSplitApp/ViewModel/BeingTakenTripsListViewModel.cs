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
using WeSplitApp.View.Controls;
namespace WeSplitApp.ViewModel
{
    public class BeingTakenTripsListViewModel : TripViewModel
    {
        private static BeingTakenTripsListViewModel instance { get; set; }
        public static BeingTakenTripsListViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BeingTakenTripsListViewModel();
                }
                return instance;
            }
        }
                                                                                               
        private BeingTakenTripsListViewModel()
        {
            IsDone = false;
            this._itemHandler = GetData();
            TripSortMethods();
        }
    }
}
