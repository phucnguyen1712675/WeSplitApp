using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WeSplitApp.ViewModel.TripDetailSlideVM;

namespace WeSplitApp.View.Controls.TripDetailSlide
{
    /// <summary>
    /// Interaction logic for Slide1_IntroControl.xaml
    /// </summary>
    public partial class Slide1_IntroControl : UserControl
    {
        public Slide1_IntroControl()
        {
            InitializeComponent();
        }

        /*private void BackSlide1Button_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().setVisibilityAddButton(Visibility.Visible);
        }*/
    }
}
