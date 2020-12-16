using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplitApp.Utils;
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for BeingTakenTripsListControl.xaml
    /// </summary>
   
    public partial class BeingTakenTripsListControl : UserControl
    {
        private static BeingTakenTripsListControl beingTakenTripList = null;

        public static BeingTakenTripsListControl GetInstance() => beingTakenTripList;
        public BeingTakenTripsListControl()
        {
            InitializeComponent();
            DataContext = BeingTakenTripsListViewModel.Instance;
            beingTakenTripList = this;
        }

        private void selectedChange(object sender, SelectionChangedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().SearchTextBox.Clear();
        }
    }
}
