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
using WeSplitApp.Resources;
namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for Search_Temp.xaml
    /// </summary>
    public partial class Search_Temp : Window
    {
        DataLocationDataContext db;
        public Search_Temp()
        {
            InitializeComponent();
            db = new DataLocationDataContext();
        }

        private void SearchLocation(object sender, RoutedEventArgs e)
        {
            _loadProducts();
        }
        public void _loadProducts()
        {
            var a = (from p in db.TRIPs
                     select p).ToList();

            if (searchBox.Text.Length <= 0)
            {

            }
            else
            {
                //var a = (from p in db.Products
                // select p).ToList();
                var requestText = convertToUnSign(searchBox.Text.Trim().ToLower());
                a = a.Where(p => p.TITTLE.ToLower().Contains(requestText)).ToList();
                //var query = db.TRIPs.Where(each_row => each_row.TITTLE.Contains(requestText) || each_row.NameUnsigned.Contains(requestText));


                var query2 = db.TRIPs.Where( delegate (TRIP c)
                {
                    if (ConvertToUnSign(c.TITTLE).IndexOf(requestText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                    else
                        return false;
                }).AsQueryable();

                foreach (var index in a)
                {
                    MessageBox.Show(index.TITTLE);
                }
            }

        }
        public string convertToUnSign(string s)
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
