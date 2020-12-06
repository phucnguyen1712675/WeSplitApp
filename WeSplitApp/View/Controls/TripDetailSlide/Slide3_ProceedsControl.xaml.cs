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
    /// Interaction logic for Slide3_ProceedsControl.xaml
    /// </summary>
    public partial class Slide3_ProceedsControl : UserControl, INotifyPropertyChanged
    {
        public static RoutedCommand HomeScreenCommand { get; set; }
        public Slide3_ProceedsControl()
        {
            InitializeComponent();
            HomeScreenCommand = new RoutedCommand();
            CommandBindings.Add(new CommandBinding(HomeScreenCommand, HomeScreenExecuted));
        }
        private void HomeScreenExecuted(object sender, ExecutedRoutedEventArgs e) => HomeScreen.GetHomeScreenInstance().SetContentControl(new TripsCollectionViewModel());

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
