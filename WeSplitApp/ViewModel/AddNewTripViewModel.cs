using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplitApp.View;
using WeSplitApp.View.DialogHelper;
using WeSplitApp.ViewModel.DialogHelperClass;

namespace WeSplitApp.ViewModel
{
    public class AddNewTripViewModel : StepperBaseViewModel
    {
        public List<IStep> Steps
        {
            get
            {
                return new List<IStep>()
                {
                    new StepOneViewModel() { Header = new StepTitleHeader() { FirstLevelTitle = "Thông tin chuyến đi" }, Content = new StepOneViewModel() },
                    new StepTwoViewModel() { Header = new StepTitleHeader() { FirstLevelTitle = "Điểm dừng - Chi phí" }, Content = new StepTwoViewModel() },
                    new StepThreeViewModel() { Header = new StepTitleHeader() { FirstLevelTitle = "Thành viên - chi trả"}, Content = new StepThreeViewModel() }
                };
            }
        }

        public TRIP AddTrip { get; set; }

        public ObservableCollection<MEMBER> MEMBERs { get; set; }

        public ObservableCollection<LOCATION> LOCATIONs { get; set; }

        public static AddNewTripViewModel Instance { get; set; }

        public AddNewTripViewModel() : base()
        {
            AddTrip = new TRIP();
            AddTrip.TOTALCOSTS = 0;
            AddTrip.CURRENTPROCEEDS = 0;
            MEMBERs = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList());
            LOCATIONs = new ObservableCollection<LOCATION>(HomeScreen.GetDatabaseEntities().LOCATIONS.ToList());
            Instance = this;
        }
    }
}
