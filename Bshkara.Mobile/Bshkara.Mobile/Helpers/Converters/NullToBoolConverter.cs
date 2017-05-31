using System;
using System.Globalization;
using Xamarin.Forms;

namespace Bshkara.Mobile.Helpers.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            if (stringValue != null)
            {
                return !string.IsNullOrWhiteSpace(stringValue);
            }

            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}