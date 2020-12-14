using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeSplitApp.Converters
{
    public class TripCostToAvarageMoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TRIP selectedTrip = (TRIP)value;
            var result = 0.0;

            if (selectedTrip != null)
            {
                var total = selectedTrip.TOTALCOSTS;
                var memberNumber = selectedTrip.TRIP_MEMBER.Count;

                result = total / memberNumber;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
