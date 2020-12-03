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
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool status = (bool)value;
            //return status ? (Color)ColorConverter.ConvertFromString("#01BF3A") : (Color)ColorConverter.ConvertFromString("#991101"); ;
            return status ? new SolidColorBrush(Color.FromArgb(0xFF, 0x01, 0xBF, 0x3A)) : new SolidColorBrush(Color.FromArgb(0xFF, 0x99, 0x11, 0x01)); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
