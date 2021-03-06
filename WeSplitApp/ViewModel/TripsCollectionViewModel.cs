﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeSplitApp.View;
using WeSplitApp.View.Controls;
namespace WeSplitApp.ViewModel
{
    public class TripsCollectionViewModel : ViewModel
    {
        //public int SelectedIndex { get; set; }
        public HaveTakenTripsListViewModel HaveTakenTripsListViewModel { get; set; }
        public BeingTakenTripsListViewModel BeingTakenTripsListViewModel { get; set; }
        private int _selectedIndex;
        public static int index;
        public int SelectedIndex
        {
            get => this._selectedIndex;
            set
            {
                this._selectedIndex = value;
                //Goi ham gi do khi Change
                index = SelectedIndex;
                HomeScreen.GetHomeScreenInstance().SearchTextBox.Text = " ";
                HomeScreen.GetHomeScreenInstance().SearchTextBox.Clear();
                HomeScreen.GetHomeScreenInstance().SearchByComboBox.SelectedIndex = 0;



            }
        }

        public TripsCollectionViewModel()
        {
            this.SelectedIndex = 0;
            this.HaveTakenTripsListViewModel = HaveTakenTripsListViewModel.Instance;
            this.BeingTakenTripsListViewModel = BeingTakenTripsListViewModel.Instance;
        }
    }
}
