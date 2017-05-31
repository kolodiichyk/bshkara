using System;
using System.Globalization;
using Xamarin.Forms;

namespace Bshkara.Mobile.Helpers.Converters
{
    public class YearsFromDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as DateTime?;
            if (source != null)
            {
                var now = DateTime.Today;
                return now.Year - source.Value.Year;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}