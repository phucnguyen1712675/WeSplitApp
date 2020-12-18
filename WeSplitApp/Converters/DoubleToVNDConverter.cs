using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class DoubleToVNDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            double money = (double)value;

            string result;

            if (money == 0.0)
            {
                result = "0";
            }
            else
            {
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                result = money.ToString("#,###", cul.NumberFormat);
            }
            return $"{result} đồng";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string money = (string)value;
            var badWordList = new List<string>()
            {
                " đồng",
                "."
            };
            foreach (string badWord in badWordList)
            {
                money = money.Replace(badWord, string.Empty);
            }
            var result = double.Parse(money, CultureInfo.InvariantCulture);
            return result;
        }
    }
}
