﻿using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeSplitApp.View.DialogHelper;
using WeSplitApp.ViewModel;
using WeSplitApp.ViewModel.DialogHelperClass;
using WeSplitApp.View.Controls;
using WeSplitApp.View.Controls.TripDetailSlide;

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
        public static INavigationItem selectedINavigationItem { get; set; }

        public static Snackbar Snackbar = new Snackbar();

        private WESPLITAPPEntities database = new WESPLITAPPEntities();

        private static HomeScreen homeScreen = null;

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

        private static void ClosingMemberDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            HomeScreen.homeScreen.AddButton.Visibility = Visibility.Visible;
        }

        private static void ClosingLocationDialogEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
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
            InitializeComponent();
            this.m_navigationItems = new List<INavigationItem>()
            {
                new FirstLevelNavigationItem() { Label = "Các chuyến đi", Icon = PackIconKind.MapMarker, NavigationItemSelectedCallback = item => new TripsCollectionViewModel()},
                new FirstLevelNavigationItem() { Label = "Danh sách thành viên", Icon = PackIconKind.AccountMultipleOutline, NavigationItemSelectedCallback = item =>MemberListViewModel.Instance},
                new FirstLevelNavigationItem() { Label = "Danh sách điểm dừng", Icon = PackIconKind.MapMarkerStar, NavigationItemSelectedCallback = item => LocationListViewModel.Instance},
                new FirstLevelNavigationItem() { Label = "Cài đặt", Icon = PackIconKind.Settings, NavigationItemSelectedCallback = item => SettingsViewModel.Instance},
                new FirstLevelNavigationItem() { Label = "Về chúng tôi", Icon = PackIconKind.About, NavigationItemSelectedCallback = item => new AboutUsViewModel()},
            };

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
        public static WESPLITAPPEntities GetDatabaseEntities() => homeScreen.database; //TODO
        public static void SetNewContentControl(object newContent) => homeScreen.SetContentControl(newContent);
        private void LoadedHandler(object sender, RoutedEventArgs args)
        {
            navigationDrawerNav.SelectedItem = m_navigationItems[0];
            m_navigationItems[0].IsSelected = true;
            selectedINavigationItem = m_navigationItems[0];
        }
        public void setVisibilityAddButton(Visibility visibility) => AddButton.Visibility = visibility;
        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args) => SelectNavigationItem(args.NavigationItem);
        public void SetContentControl(object newContent) => contentControl.Content = newContent;
        public static void RestoreNavigationItem() => homeScreen.SelectNavigationItem(selectedINavigationItem);

        private void SelectNavigationItem(INavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                object newContent = navigationItem.NavigationItemSelectedCallback(navigationItem);

                if (contentControl.Content == null || contentControl.Content.GetType() != newContent.GetType())
                {
                    SetContentControl(newContent);
                    AddButton.Visibility = Visibility.Visible;
                    selectedINavigationItem = navigationItem;
                }
            }
            else
            {
                SetContentControl(null);
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
            AddButton.Visibility = Visibility.Collapsed;
            string v = "MemberAddDialog";
            GetDialogs(v, new MEMBER(), "THÊM THÀNH VIÊN");
        }

        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Collapsed;
            string v = "LocationAddDialog";
            GetDialogs(v, new LOCATION(), "THÊM ĐIỂM DỪNG");
        }

        private void AddTripButton_Click(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Collapsed;
            navigationDrawerNav.SelectedItem = null;
            contentControl.Content = new AddNewTripViewModel();
        }

        public static void SetNavigationDrawerNavNull() => homeScreen.navigationDrawerNav.SelectedItem = null;

        private void SearchEvent(object sender, TextChangedEventArgs e)
        {
            string typeSearch = HomeScreen.GetHomeScreenInstance().SearchByComboBox.Text;
            int option = 0;
            for (int i = 0; i < 3; i++)
            {
                if (NavigationItems[i].IsSelected)
                {
                    option = i;
                    break;
                }
            }
            switch (option)
            {
                case 0:

                    if (TripsCollectionViewModel.index == 0)
                    {
                        HaveTakenTripsListViewModel.Instance.search_byTripName();
                    }
                    else
                    {
                        BeingTakenTripsListViewModel.Instance.search_byTripName();
                    }
                    break;
                case 1:
                    MemberListViewModel.Instance.searchMember_ByName();
                    break;
                case 2:
                    LocationListViewModel.Instance.searchLocation_ByName();
                    //giao dien member
                    break;
            }

        }

        private void MaterialWindow_Loaded(object sender, RoutedEventArgs e)
        {
             SettingsViewModel.Instance.LoadAll();
        }

        private void MaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SettingsViewModel.Instance.SaveAll();
        }

        private void resetSearchBox(object sender, SelectionChangedEventArgs e)
        {
            HomeScreen.GetHomeScreenInstance().SearchTextBox.Clear();

        }
    }
}
