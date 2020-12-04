using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    class LocationListViewModel : ViewModel
    {
        public ObservableCollection<LOCATION> _lOCATIONS;
        public ObservableCollection<LOCATION> LOCATIONS { 
            get => this._lOCATIONS; 
            set
            {
                this._lOCATIONS = value;
                OnPropertyChanged();
            }
        }
        private static LocationListViewModel instance { get; set; }
        /*public ObservableCollection<LOCATION> LOCATIONS { get; set; }*/
        public LocationListViewModel(ObservableCollection<LOCATION> LOCATIONS)
        {
           // this.LOCATIONS = new ObservableCollection<LOCATION>(HomeScreen.GetDatabaseEntities().LOCATIONS.ToList());
           this.LOCATIONS = LOCATIONS;
            instance = this;
        }

        internal static void updateList(LOCATION newLocation)
        {
            if (instance != null)
                instance.LOCATIONS.Add(newLocation);
        }
    }
}
