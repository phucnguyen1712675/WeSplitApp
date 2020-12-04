using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WeSplitApp.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string relative = (string)value;
            if(relative == null) return relative;
            if (relative.Contains(AppDomain.CurrentDomain.BaseDirectory)) return relative;
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = $"{folder}{relative}";
            return absolutePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bitMap = (BitmapImage)value;
            string uri = bitMap.UriSource.ToString().Replace("file:///", "").Replace("/","\\");
            if (uri.Contains(AppDomain.CurrentDomain.BaseDirectory))  uri = "\\" + uri.Remove(0, AppDomain.CurrentDomain.BaseDirectory.Length);
            return uri;
        }
    }
}
