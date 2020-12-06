using System;
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

namespace WeSplitApp.View.Controls
{
    /// <summary>
    /// Interaction logic for TripsCollectionControl.xaml
    /// </summary>
    public partial class TripsCollectionControl : UserControl
    {
        public TripsCollectionControl()
        {
            InitializeComponent();
        }

        private void ChangeWindow(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("dung roi");

        }
    }
}
