using System;
using System.Globalization;
using Xamarin.Forms;

namespace Bshkara.Mobile.Helpers.Converters
{
    public class ToFormatedDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var format = parameter as string;
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "MMM dd, yyyy";
            }
            var date = (DateTime) value;

            return date.ToString(format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}