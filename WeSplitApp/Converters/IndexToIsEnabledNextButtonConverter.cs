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
    public class IndexToIsEnabledNextButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var selectedIndex = (int)values[0];
            var imagesCount = (int)values[1];
            var currentPage = (int)values[2];
            var totalPage = (int)values[3];
            var rowPerPage = (int)values[4];
            var isLastPage = currentPage == totalPage;
            var isLastImage = selectedIndex + 1 + rowPerPage * (currentPage - 1) == imagesCount;
            // 0 1 2 3 
            //4(0) 5(1) 6(2) 7(3)
            return !(isLastPage && isLastImage);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
