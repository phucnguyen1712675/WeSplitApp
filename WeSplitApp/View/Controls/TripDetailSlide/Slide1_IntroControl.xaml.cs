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

namespace WeSplitApp.View.Controls.TripDetailSlide
{
    /// <summary>
    /// Interaction logic for Slide1_IntroControl.xaml
    /// </summary>
    public partial class Slide1_IntroControl : UserControl, INotifyPropertyChanged
    {
        public static RoutedCommand BackCommand { get; set; }
        public Slide1_IntroControl()
        {
            InitializeComponent();
            BackCommand = new RoutedCommand();
            CommandBindings.Add(new CommandBinding(BackCommand, BackExecuted));
        }
        private void BackExecuted(object sender, ExecutedRoutedEventArgs e) => HomeScreen.GetHomeScreenInstance().SetContentControl(new TripsCollectionViewModel());

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void BackSlide1Button_Click(object sender, RoutedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().setVisibilityAddButton(Visibility.Visible);
        }
    }
}
