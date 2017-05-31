using System;
using System.Web;

namespace Bshkara.Web.Helpers
{
    public static class TimeZoneOffsetHelper
    {
        public static string COOKIE_TIME_ZONE_OFFSET_KEY = "_OdriTimeZoneOffset";
            // to not conflict with angular and other frameorks variables

        public static TimeSpan GetTimeZoneOffsetFromCookies()
        {
            var result = TimeSpan.FromMinutes(0); // Default offset (Utc) if cookie is missing.
            var timeZoneCookie = HttpContext.Current.Request.Cookies[COOKIE_TIME_ZONE_OFFSET_KEY];
            if (timeZoneCookie != null)
            {
                double offsetMinutes = 0;
                if (double.TryParse(timeZoneCookie.Value, out offsetMinutes))
                {
                    // Store in TimeZoneOffset. You can use Session, TempData, or anything else.
                    result = TimeSpan.FromMinutes(offsetMinutes);
                }
            }

            return result;
        }
    }
}