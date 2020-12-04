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
    /// Interaction logic for ImagePresenterControl.xaml
    /// </summary>
    public partial class ImagePresenterControl : UserControl
    {
        public ImagePresenterControl()
        {
            InitializeComponent();

            this.DataContext = new ImagePresenterViewModel(TripDetailsViewModel.SelectedTrip);
        }
    }
}