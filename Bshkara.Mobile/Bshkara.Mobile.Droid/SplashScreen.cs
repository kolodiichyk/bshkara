using Android.App;
using Android.Content.PM;
using Android.OS;
using Bshkara.Mobile.Droid;

namespace MOH.Droid
{
    [Activity(
        Label = "Bshkara",
        Theme = "@style/Theme.Splash",
        Icon = "@drawable/icon",
        NoHistory = true,
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.Locale | ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class StartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof (MainActivity));
            OverridePendingTransition(0, 0);
        }
    }
}