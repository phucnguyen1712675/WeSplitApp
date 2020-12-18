
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
    public class IndexToVisibilityForTripCostConverter : IValueConverter
    {
        public bool IsReversed { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)value;
            var isOne = index == 1;
            if (IsReversed)
            {
                isOne = !isOne;
            }
            var visibility = isOne ? Visibility.Collapsed : Visibility.Visible;
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
