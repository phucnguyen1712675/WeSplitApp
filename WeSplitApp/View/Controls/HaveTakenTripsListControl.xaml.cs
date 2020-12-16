﻿using System;
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
using WeSplitApp.ViewModel;

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for HaveTakenTripsListControl.xaml
    /// </summary>
    public partial class HaveTakenTripsListControl : UserControl
    {
        private static HaveTakenTripsListControl haveTakenTripList = null;

        public static HaveTakenTripsListControl GetInstance() => haveTakenTripList;

        public HaveTakenTripsListControl()
        {
            InitializeComponent();
            DataContext = HaveTakenTripsListViewModel.Instance;
            haveTakenTripList = this;
        }

        private void selectedChange(object sender, SelectionChangedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().SearchTextBox.Clear();
        }
    }
}
 