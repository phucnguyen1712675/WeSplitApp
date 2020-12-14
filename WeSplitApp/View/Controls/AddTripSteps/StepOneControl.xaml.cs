using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeSplitApp.ViewModel;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace WeSplitApp.View.Controls.AddTripSteps
{
    /// <summary>
    /// Interaction logic for StepOneControl.xaml
    /// </summary>
    public partial class StepOneControl : UserControl
    {
        public StepOneControl()
        {
            InitializeComponent();
            this.DataContext = AddNewTripViewModel.Instance;
        }

        private void TripAddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                                 "All files (*.*)|*.*";
            fileDialog.Title = "My Image Browser";
            DialogResult dr = fileDialog.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string imagePath = fileDialog.FileName;
                //var folder = AppDomain.CurrentDomain.BaseDirectory;

                var bitmap = new BitmapImage(
                    new Uri(imagePath, UriKind.Absolute)
                );
                TripImage.Source = bitmap;
            }
        }

        private void TripAddListImagesButton_Click(object sender, RoutedEventArgs e)
        {
            var temp = AddNewTripViewModel.Instance.AddTrip.TRIP_IMAGES;
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
        "All files (*.*)|*.*";

            fileDialog.Multiselect = true;
            fileDialog.Title = "My Image Browser";

            DialogResult dr = fileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                List<string> result = new List<string>();
                foreach (string file in fileDialog.FileNames)
                {
                    try
                    {
                        result.Add(file);
                    }
                    catch (SecurityException ex)
                    {
                        System.Windows.MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace
                                );
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
                if (result != null)
                {
                    var query = from item in result select new TRIP_IMAGES { IMAGE = item };
                    AddNewTripViewModel.Instance.AddTrip.TRIP_IMAGES.Clear();

                    foreach (var item in query)
                    {
                        AddNewTripViewModel.Instance.AddTrip.TRIP_IMAGES.Add(item);
                    }
                    var tempItemSource = TripImageListView.ItemsSource;
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn ảnh");
                }
            }
        }

    }
}
