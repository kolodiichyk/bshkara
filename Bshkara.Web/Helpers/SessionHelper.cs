using System.Web;

namespace Bshkara.Web.Helpers
{
    public static class SessionHelper
    {
        public static T Get<T>(string key, T defaultValue = default(T))
        {
            var value = HttpContext.Current.Session[key];

            return value == null ? defaultValue : (T) value;
        }

        public static void Set<T>(string key, T value)
        {
            if (value == null)
            {
                Clean(key);
            }
            else
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        public static void Clean(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}