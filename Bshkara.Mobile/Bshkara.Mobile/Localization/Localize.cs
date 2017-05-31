using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;

namespace Bshkara.Mobile.Localization
{
    public class Localize
    {
        private static readonly CultureInfo ci;

        static Localize()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public static string GetString(string key, string comment)
        {
            var temp = new ResourceManager("Bshkara.Mobile.Localization.Resources.AppResources",
                typeof (Localize).GetTypeInfo().Assembly);

            var result = temp.GetString(key, ci);

            return result;
        }
    }
}