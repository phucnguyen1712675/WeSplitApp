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
    public class TripCostsToMoneyEachPersonConverter : IMultiValueConverter
    {
        public IMultiValueConverter Converter1 { get; set; }
        public IValueConverter Converter2 { get; set; }
        public IValueConverter Converter3 { get; set; }

        #region IValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var avarageMoney = Converter1.Convert(values, targetType, parameter, culture);
            var MoneyString = Converter2.Convert(avarageMoney, targetType, parameter, culture);
            var result = Converter3.Convert(MoneyString, targetType, parameter, culture);
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
