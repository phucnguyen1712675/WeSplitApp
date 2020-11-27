using MahApps.Metro.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeSplitApp.ViewModel;

namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : MaterialWindow
    {
        public const string DialogHostName = "dialogHost";

        public DialogHost DialogHost => m_dialogHost;

        private List<INavigationItem> m_navigationItems;
        public List<INavigationItem> NavigationItems => m_navigationItems;

        public static Snackbar Snackbar = new Snackbar();

        public HomeScreen()
        {
            m_navigationItems = new List<INavigationItem>()
            {
                new FirstLevelNavigationItem() { Label = "Đã kết thúc", Icon = PackIconKind.Passport, NavigationItemSelectedCallback = item => new HaveTakenTripsListViewViewModel()},
                new FirstLevelNavigationItem() { Label = "Đang đi/Sắp tới", Icon = PackIconKind.Plane, NavigationItemSelectedCallback = item => new BeingTakenTripsListViewViewModel()},
                //new FirstLevelNavigationItem() { Label = "Thêm mới", Icon = PackIconKind.Add, NavigationItemSelectedCallback = item => new AddNewTripViewModel()},
                new SubheaderNavigationItem() { Subheader = "Thành viên"},
                new FirstLevelNavigationItem() { Label = "Danh sách thành viên", Icon = PackIconKind.AccountMultipleOutline, NavigationItemSelectedCallback = item => new HaveTakenTripsListViewViewModel()},
                new SubheaderNavigationItem() { Subheader = "Điểm dừng"},
                new FirstLevelNavigationItem() { Label = "Danh sách điểm dừng", Icon = PackIconKind.MapMarkerStar, NavigationItemSelectedCallback = item => new HaveTakenTripsListViewViewModel()},
                new SubheaderNavigationItem() { Subheader = "Khác"},
                new FirstLevelNavigationItem() { Label = "Cài đặt", Icon = PackIconKind.Settings, NavigationItemSelectedCallback = item => new SettingsViewModel()},
                new FirstLevelNavigationItem() { Label = "Về chúng tôi", Icon = PackIconKind.About, NavigationItemSelectedCallback = item => new AboutUsViewModel()},
            };

            InitializeComponent();

            Task.Factory.StartNew(() => Thread.Sleep(2500)).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue?.Enqueue("Welcome to We Split App");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            Snackbar = MainSnackbar;
            navigationDrawerNav.DataContext = this;

            Loaded += LoadedHandler;
        }

        private void LoadedHandler(object sender, RoutedEventArgs args)
        {
            navigationDrawerNav.SelectedItem = m_navigationItems[0];
            //sideNav.SelectedItem = m_navigationItems[0];
            m_navigationItems[0].IsSelected = true;
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            SelectNavigationItem(args.NavigationItem);
        }

        private void SelectNavigationItem(INavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                object newContent = navigationItem.NavigationItemSelectedCallback(navigationItem);

                if (contentControl.Content == null || contentControl.Content.GetType() != newContent.GetType())
                {
                    contentControl.Content = newContent;
                }
            }
            else
            {
                contentControl.Content = null;
            }

            if (appBar != null)
            {
                appBar.IsNavigationDrawerOpen = false;
            }
        }

        private void GoToGitHubButtonClickHandler(object sender, RoutedEventArgs args) => OpenLink("https://github.com/phucnguyen1712675/WeSplitApp");

        private void OpenLink(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO add member
        }

        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO add location
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO add Trip
            contentControl.Content = new AddNewTripViewModel();
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO closing handle if choose goodbye

        }

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
