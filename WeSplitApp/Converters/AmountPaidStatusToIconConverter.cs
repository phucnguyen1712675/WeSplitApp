using MaterialDesignThemes.Wpf;
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
    public class AmountPaidStatusToIconConverter : IMultiValueConverter
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

            var icon = isNotEqual? PackIconKind.CloseCircleOutline : PackIconKind.CheckCircleOutline;

            return icon;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
