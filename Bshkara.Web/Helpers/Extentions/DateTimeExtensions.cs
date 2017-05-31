using System;

namespace Bshkara.Web.Helpers.Extentions
{
    public static class DateTimeExtensions
    {
        public static string DATETIME_FORMAT = "dd.MM.yyyy HH:mm";
        public static string DATE_FORMAT = "dd.MM.yyyy";
        public static string TIME_FORMAT = "HH:mm";

        public static DateTime ToLocalDateTime(this DateTime utcDateTime)
        {
            try
            {
                return utcDateTime.Add(TimeZoneOffsetHelper.GetTimeZoneOffsetFromCookies());
            }
            catch (Exception)
            {
                return utcDateTime;
            }
        }

        public static string DisplayDateTime(this DateTime utcDateTime)
        {
            return utcDateTime.ToLocalDateTime().ToString();
        }

        public static string StringFormat(this DateTime utcDateTime)
        {
            return utcDateTime.ToLocalDateTime().ToString(DATETIME_FORMAT);
        }

        public static string StringLocalDateFormat(this DateTime utcDateTime)
        {
            return utcDateTime.ToLocalDateTime().ToString(DATE_FORMAT);
        }

        public static string StringDateFormat(this DateTime utcDateTime)
        {
            return utcDateTime.ToString(DATE_FORMAT);
        }

        public static string StringTimeFormat(this DateTime utcDateTime)
        {
            return utcDateTime.ToLocalDateTime().ToString(TIME_FORMAT);
        }

        public static DateTime ToDateTimeFromUnixTimestamp(double timeStamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timeStamp);
        }

        public static double ToUnixTimeStamp(this DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}