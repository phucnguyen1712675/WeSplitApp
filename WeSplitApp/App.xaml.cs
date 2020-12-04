using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WeSplitApp.View;
using WeSplitApp.ViewModel;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void showHomeScreen()
        {
            //var homeScreen = new HomeScreen();
            //this.MainWindow = homeScreen;
            //homeScreen.Show();
            var SearchScreen = new Search_Temp();
            //this.MainWindow = SearchScreen;
            SearchScreen.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var value = ConfigurationManager.AppSettings["isSplashScreenAllowed"];
            var isSplashScreenAllowed = bool.Parse(value);

            if (isSplashScreenAllowed)
            {
                //initialize the splash screen and set it as the application main window
                var splashScreen = new View.SplashScreen();
                this.MainWindow = splashScreen;
                splashScreen.Show();

                //in order to ensure the UI stays responsive, we need to
                //do the work on a different thread
                Task.Factory.StartNew(() =>
                {
                    //we need to do the work in batches so that we can report progress
                    for (var i = 1; i <= 100; i++)
                    {
                        //simulate a part of work being done
                        Thread.Sleep(30);

                        //because we're not on the UI thread, we need to use the Dispatcher
                        //associated with the splash screen to update the progress bar
                        splashScreen.Dispatcher.Invoke(() => splashScreen.ProgressBar = i);
                    }

                    //once we're done we need to use the Dispatcher
                    //to create and show the main window
                    this.Dispatcher.Invoke(() =>
                    {
                        //initialize the main window, set it as the application main window
                        //and close the splash screen
                        showHomeScreen();
                        splashScreen.Close();
                    });
                });
            }
            else
            {
                showHomeScreen();
            }
        }
    }
}
