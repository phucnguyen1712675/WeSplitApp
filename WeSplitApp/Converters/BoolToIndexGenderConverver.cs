using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class BoolToIndexGenderConverver : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool gender = (bool)value;
            var isMale = gender ? 0 : 1;
            return isMale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int gender = (int)value;
            var isMale = gender != 1;
            return isMale;
        }
    }
}
