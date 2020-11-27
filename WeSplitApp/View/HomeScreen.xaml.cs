using MahApps.Metro.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            /*sideNav.DataContext = this;*/
            navigationDrawerNav.DataContext = this;

            Loaded += LoadedHandler;
        }

        private void LoadedHandler(object sender, RoutedEventArgs args)
        {
            navigationDrawerNav.SelectedItem = m_navigationItems[1];
           /* sideNav.SelectedItem = m_navigationItems[1];*/
            m_navigationItems[1].IsSelected = true;
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
    }
}
