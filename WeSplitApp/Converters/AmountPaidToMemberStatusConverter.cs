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
    public class AmountPaidToMemberStatusConverter : IMultiValueConverter
    {
        public IMultiValueConverter Converter1 { get; set; }
        public IValueConverter Converter2 { get; set; }
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

            string status;
            if (isNotEqual)
            {
                object missingAmountConvertedValue = Converter2.Convert(missingAmount, targetType, parameter, culture);
                status = $"Thiếu: {missingAmountConvertedValue}";
            }
            else
            {
                status = "Đã thu đủ";
            }

            return status;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
