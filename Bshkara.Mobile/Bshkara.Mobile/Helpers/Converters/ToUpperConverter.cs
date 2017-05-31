using System;
using System.Globalization;
using Xamarin.Forms;

namespace Bshkara.Mobile.Helpers.Converters
{
	public class ToUpperConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var source = value as string;
			if (string.IsNullOrWhiteSpace(source))
			{
				return null;
			}

			return source.ToUpper();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
