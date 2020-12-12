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
    public class HaveTakenTripsListViewModel : TripViewModel
    {
        private static HaveTakenTripsListViewModel instance { get; set; }
        public static HaveTakenTripsListViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HaveTakenTripsListViewModel();
                }
                return instance;
            }
        }

        private HaveTakenTripsListViewModel()
        {
            IsDone = true;
            this._itemHandler = GetData();

            TripSortMethods();
        }

        

    }
}
