using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WeSplitApp.Converters
{
    public class AmountPaidStatusToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(values[0].ToString()) || string.IsNullOrEmpty(values[1].ToString()))
            {
                return null;
            }
            const double precise = 0.0000001;
            var averageMoney = double.Parse(values[0].ToString());
            var amountPaid = (double)values[1];
            var missingAmount = averageMoney - amountPaid;

            var statusColor = Math.Abs(missingAmount) >= precise && missingAmount >= 0.0 ? new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x28, 0x28)) : new SolidColorBrush(Color.FromArgb(0xFF, 0x2E, 0x7D, 0x32));

            return statusColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
