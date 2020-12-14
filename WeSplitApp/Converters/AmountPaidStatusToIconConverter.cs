using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class AmountPaidStatusToIconConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(values[0].ToString()) || string.IsNullOrEmpty(values[1].ToString()))
            {
                return PackIconKind.Abc;
            }
            const double precise = 0.0000001;
            var averageMoney = double.Parse(values[0].ToString());
            var amountPaid = (double)values[1];
            var missingAmount = averageMoney - amountPaid;

            var icon = Math.Abs(missingAmount) >= precise ? PackIconKind.CloseCircleOutline : PackIconKind.CheckCircleOutline;

            return icon;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
