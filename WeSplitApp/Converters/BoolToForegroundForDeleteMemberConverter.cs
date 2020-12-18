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
    public class BoolToForegroundForDeleteMemberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isDeletable = (bool)value;
            var foreground = isDeletable ? new SolidColorBrush(Color.FromArgb(0xFF, 0x01, 0xBF, 0x3A)) : new SolidColorBrush(Color.FromArgb(0xFF, 0x99, 0x11, 0x01));
            return foreground;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
