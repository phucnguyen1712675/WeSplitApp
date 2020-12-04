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
using System.Windows.Shapes;
using WeSplitApp.View;
namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for Search_Temp.xaml
    /// </summary>
    public partial class Search_Temp : Window
    {

        public Search_Temp()
        {
            InitializeComponent();
        }

        private void SearchLocation(object sender, RoutedEventArgs e)
        {
            _loadProducts();
        }
        public void _loadProducts()
        {
            

            if (searchBox.Text.Length <= 0)
            {

            }
            else
            {
                var requestText = convertToUnSign2(searchBox.Text.Trim().ToLower());
                var requestText2 = convertToUnSign2(DecriptBox.Text.Trim().ToLower());
                //search by TITLE
                var b = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertToUnSign2(t.TITTLE).Contains(requestText))).ToList();

                //SEARCH BY DECRIPTION
                var c = (HomeScreen.GetDatabaseEntities().TRIPS.AsEnumerable().Where(t => convertToUnSign2(t.DESCRIPTION).Contains(requestText2))).ToList();
                foreach (var index in c)
                {
                    MessageBox.Show(index.TITTLE);
                }
            }

        }
        public string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
    }
}
