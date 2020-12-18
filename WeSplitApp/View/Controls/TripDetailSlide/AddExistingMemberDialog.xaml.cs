using MaterialDesignThemes.Wpf;
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

namespace WeSplitApp.View.Controls.TripDetailSlide
{
    /// <summary>
    /// Interaction logic for AddExistingMemberDialog.xaml
    /// </summary>
    public partial class AddExistingMemberDialog : UserControl
    {
        public static AddExistingMemberDialog instance { get; set; }
        public AddExistingMemberDialog()
        {
            InitializeComponent();
            instance = this;
        }

        public static void updateByMemberListComboBox()
        {
            if(instance != null)
                instance.ByMemberListComboBox.Items.Refresh();
        }

        private void ClearSlectedByMemberButton_Click(object sender, RoutedEventArgs e)
        {
            ByMemberListComboBox.SelectedItem = null;
        }
    }
}
