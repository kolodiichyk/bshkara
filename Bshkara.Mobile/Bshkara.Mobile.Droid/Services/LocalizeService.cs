using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Net;
using Android.Provider;
using Bshkara.Mobile.Droid.Services;
using Bshkara.Mobile.Localization;
using Java.Util;
using Xamarin.Forms;
using XLabs.Ioc;

[assembly: Dependency(typeof (LocalizeService))]

namespace Bshkara.Mobile.Droid.Services
{
    public class LocalizeService : ILocalize
    {
        private BootCompletedBroadcastMessageReceiver _br;

        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");
            return new CultureInfo(netLanguage);
        }

        public async Task<CultureInfo> SetLocale()
        {
            _br = new BootCompletedBroadcastMessageReceiver();
            Forms.Context.RegisterReceiver(_br, new IntentFilter(Intent.ActionLocaleChanged));

            var ci = GetCurrentCultureInfo();

            var activity = Forms.Context as MainActivity;
            if (activity != null)
            {
                var intent = activity.Intent;
                Forms.Context.StartActivity(new Intent(Settings.ActionLocaleSettings));

                while (!_br.IsComplited)
                {
                    await Task.Delay(500);
                }

                _br.Reset();
                activity.Finish();
                activity.StartActivity(intent);
            }

            return ci;
        }

        public bool IsRightToLeft => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
    }

    public class BootCompletedBroadcastMessageReceiver : BroadcastReceiver
    {
        public BootCompletedBroadcastMessageReceiver()
        {
            IsComplited = false;
        }

        public bool IsComplited { get; private set; }

        public Uri Uri { get; private set; }

        public override void OnReceive(Context context, Intent intent)
        {
            IsComplited = true;

            var ci = Resolver.Resolve<ILocalize>().GetCurrentCultureInfo();
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public void Reset()
        {
            IsComplited = false;
        }
    }
}