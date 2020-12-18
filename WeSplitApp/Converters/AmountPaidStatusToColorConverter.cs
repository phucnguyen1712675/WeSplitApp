using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WeSplitApp.Converters
{
    public class AmountPaidStatusToColorConverter : IMultiValueConverter
    {
        public IMultiValueConverter Converter1 { get; set; }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
            {
                return DependencyProperty.UnsetValue;
            }
            var averageMoney = Converter1.Convert(values, targetType, parameter, culture);
            var amountPaid = (double)values[2];
            var missingAmount = (double)averageMoney - amountPaid;
            const double precise = 0.0000001;
            var isNotEqual = Math.Abs(missingAmount) >= precise && missingAmount >= 0.0;
            var statusColor = isNotEqual? new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x28, 0x28)) : new SolidColorBrush(Color.FromArgb(0xFF, 0x2E, 0x7D, 0x32));

            return statusColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
