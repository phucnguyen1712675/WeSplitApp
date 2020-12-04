using MahApps.Metro.Controls;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WeSplitApp.View.DialogHelper;
using WeSplitApp.ViewModel;
using WeSplitApp.ViewModel.DialogHelperClass;

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

        public WESPLITAPPEntities database = new WESPLITAPPEntities();

        public static HomeScreen homeScreen = null;

        public Dictionary<string, Delegate> Dialogs = new Dictionary<string, Delegate>
        {
            {"MemberAddDialog", new Action<MEMBER, string>(OpenMemberAddDialog)},
            {"LocationAddDialog", new Action<LOCATION, string>(OpenLocationAddDialog)},
        };

        #region Open Dialog
        private async static void OpenLocationAddDialog(LOCATION location, string tittle)
        {
            var view = new LocationAddDialog
            {
                DataContext = new LocationAddDialogViewModel(location, tittle)
            };

            var result = await DialogHost.Show(view, ClosingLocationDialogEventHandler);

            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private async static void OpenMemberAddDialog(MEMBER member, string tittle)
        {
            var view = new MemberAddDialog
            {
                DataContext = new MemberAddDialogViewModel(member, tittle)
            };

            var result = await DialogHost.Show(view, ClosingMemberDialogEventHandler);

            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));

        }

        private static void ClosingMemberDialogEventHandler(object sender, DialogClosingEventArgs eventArgs) {
            HomeScreen.homeScreen.AddButton.Visibility = Visibility.Visible;
        }

        private static void ClosingLocationDialogEventHandler(object sender, DialogClosingEventArgs eventArgs) {
            HomeScreen.homeScreen.AddButton.Visibility = Visibility.Visible;
        }

        internal void GetDialogs(string v, Object o, string tittle)
        {
            if (Dialogs.ContainsKey(v))
            {
                Dialogs[v].DynamicInvoke(o, tittle);
            }
        }

        #endregion
        public HomeScreen()
        {
            homeScreen = this;
            m_navigationItems = new List<INavigationItem>()
            {
                new FirstLevelNavigationItem() { Label = "Đã kết thúc", Icon = PackIconKind.Passport, NavigationItemSelectedCallback = item => new HaveTakenTripsListViewViewModel()},
                new FirstLevelNavigationItem() { Label = "Đang đi/Sắp tới", Icon = PackIconKind.Plane, NavigationItemSelectedCallback = item => new BeingTakenTripsListViewViewModel()},
                new SubheaderNavigationItem() { Subheader = "Thành viên"},
                new FirstLevelNavigationItem() { Label = "Danh sách thành viên", Icon = PackIconKind.AccountMultipleOutline, NavigationItemSelectedCallback = item => new MemberListViewModel(new ObservableCollection<MEMBER>(homeScreen.database.MEMBERS.ToList()))},
                new SubheaderNavigationItem() { Subheader = "Điểm dừng"},
                new FirstLevelNavigationItem() { Label = "Danh sách điểm dừng", Icon = PackIconKind.MapMarkerStar, NavigationItemSelectedCallback = item => new LocationListViewModel(new ObservableCollection<LOCATION>(homeScreen.database.LOCATIONS.ToList()))},
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

        public static HomeScreen GetHomeScreenInstance() => homeScreen;

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

        public void SetContentControl(object newContent) => contentControl.Content = newContent;
        

        public void setVisibilityAddButton(Visibility visibility) => AddButton.Visibility = visibility;

        private void SelectNavigationItem(INavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                object newContent = navigationItem.NavigationItemSelectedCallback(navigationItem);

                if (contentControl.Content == null || contentControl.Content.GetType() != newContent.GetType())
                {
                    contentControl.Content = newContent;
                    AddButton.Visibility = Visibility.Visible;
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
            AddButton.Visibility = Visibility.Collapsed;
            string v = "MemberAddDialog";
            GetDialogs(v, new MEMBER(),"THÊM THÀNH VIÊN");

        }

        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO add location
            //AddButton.Visibility = Visibility.Collapsed;
            AddButton.Visibility = Visibility.Collapsed;
            string v = "LocationAddDialog";
            GetDialogs(v, new LOCATION(), "THÊM ĐIỂM DỪNG");
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO add Trip
            AddButton.Visibility = Visibility.Collapsed;
            contentControl.Content = new AddNewTripViewModel();
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO closing handle if choose goodbye
            AddButton.Visibility = Visibility.Collapsed;
        }

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
