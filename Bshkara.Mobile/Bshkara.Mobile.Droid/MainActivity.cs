using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Bshkara.Mobile.Droid.Helpers;
using Bshkara.Mobile.Droid.Services;
using Bshkara.Mobile.Localization;
using Bshkara.Mobile.Services;
using Bshkara.Mobile.Services.PlatformService;
using FFImageLoading.Forms.Droid;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using HockeyApp.Android.Utils;
using Xamarin.Facebook;
using Xamarin.Forms;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.Media;
using LoginManager = Xamarin.Facebook.Login.LoginManager;
using LoginResult = Xamarin.Facebook.Login.LoginResult;

[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/app_id")]
[assembly: MetaData("com.facebook.sdk.ApplicationName", Value = "@string/app_name")]

namespace Bshkara.Mobile.Droid
{
    [Activity(Label = "Bshkara", Icon = "@drawable/icon", Theme = "@style/MainTheme",
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationAppCompatDroid
    {
        /// <summary>
        ///     Hockeyapp android application identifier
        /// </summary>
        public const string HOCKEYAPP_APPID = "b94ced2f76f446389da38947af90fd42";

        private ICallbackManager fbCallbackManager;

        internal Action SetFacebookCancellation;

        internal Action<Exception> SetFacebookLoginError;

        internal Action<AccessToken> SetFacebookLoginResult;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            Initialize();

            LoadApplication(new App());
        }

        public void Initialize()
        {
            RegisterHockeyApp();

            InitFacebook();

            CachedImageRenderer.Init();
            UserDialogs.Init(this);
            PlatformService.Init(this);
            App.OnContainerSet += RegisterNativeServices;
        }

        private void RegisterNativeServices(IDependencyContainer container)
        {
            container.Register(t => AndroidDevice.CurrentDevice);
            container.Register(t => t.Resolve<IDevice>().Display);
            container.Register<IGeolocator, Geolocator>();
            container.Register<ILocalize, LocalizeService>();
            container.Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()));
            container.Register<IMediaPicker, MediaPicker>();
            container.Register<IPlatformService, PlatformService>();
            container.Register<IFacebookLoginService, FacebookLoginService>();
            container.Register<ITwitterLoginService, TwitterLoginService>();
            container.Register(t => UserDialogs.Instance);
        }

        private void RegisterHockeyApp()
        {
            HockeyLog.LogLevel = 3;

            // Register the crash manager before Initializing the trace writer
            CrashManager.Register(this, HOCKEYAPP_APPID);

            //Register to with the Update Manager
            UpdateManager.Register(this, HOCKEYAPP_APPID);

            MetricsManager.Register(Application, HOCKEYAPP_APPID);
        }

        private void InitFacebook()
        {
            FacebookSdk.SdkInitialize(ApplicationContext);
            fbCallbackManager = CallbackManagerFactory.Create();

            var loginCallback = new FacebookCallback<LoginResult>
            {
                HandleSuccess = loginResult => { SetFacebookLoginResult(loginResult.AccessToken); },
                HandleError = ex => { SetFacebookLoginError(ex); },
                HandleCancel = () => SetFacebookCancellation()
            };

            LoginManager.Instance.RegisterCallback(fbCallbackManager, loginCallback);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            fbCallbackManager.OnActivityResult(requestCode, (int) resultCode, data);
        }
    }
}