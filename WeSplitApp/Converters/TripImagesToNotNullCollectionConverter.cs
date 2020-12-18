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
    public class TripImagesToNotNullCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var trip = (TRIP)value;

            if (trip != null)
            {
                if (trip.TRIP_IMAGES.Count < 4)
                {
                    for (int i = trip.TRIP_IMAGES.Count; i < 4; i++)
                    {
                        trip.TRIP_IMAGES.Add(new TRIP_IMAGES
                        {
                            TRIP_ID = trip.TRIP_ID,
                            IMAGE = "/Resources/Images/NullImage.png"
                        }); ;
                    }
                }
            }
            return trip;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
