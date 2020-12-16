using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class IndexToIsEnabledPreviousButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedIndex = (int)values[0];
            var currentPage = (int)values[1];
            var isFirstPage = currentPage == 1;
            var isFirstPicture = selectedIndex == 0;
            return !(isFirstPage && isFirstPicture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
