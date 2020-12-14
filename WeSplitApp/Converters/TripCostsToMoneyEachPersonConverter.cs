using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class TripCostsToMoneyEachPersonConverter : IValueConverter
    {
        public IValueConverter Converter1 { get; set; }
        public IValueConverter Converter2 { get; set; }
        public IValueConverter Converter3 { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            object convertedValue = Converter1.Convert(value, targetType, parameter, culture);

            object anotherConvertedValue = Converter2.Convert(convertedValue, targetType, parameter, culture);

            return Converter3.Convert(anotherConvertedValue, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
