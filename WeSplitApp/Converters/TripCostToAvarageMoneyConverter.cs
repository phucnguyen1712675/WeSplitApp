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
    public class TripCostToAvarageMoneyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            /*if (string.IsNullOrEmpty(values[0].ToString()) || string.IsNullOrEmpty(values[1].ToString()))
            {
                return null;
            }*/
            if (values.Any(x => x == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }

            var totalcosts = (double)values[0];
            var membercount = (int)values[1];
            var avarageMoney = totalcosts / membercount;
            return avarageMoney;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
