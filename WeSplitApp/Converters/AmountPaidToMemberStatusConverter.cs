using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class AmountPaidToMemberStatusConverter : IMultiValueConverter
    {
        public IValueConverter Converter1 { get; set; }
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

            string status;
            if (Math.Abs(missingAmount) >= precise && missingAmount >= 0.0)
            {
                object missingAmountConvertedValue = Converter1.Convert(missingAmount, targetType, parameter, culture);
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
