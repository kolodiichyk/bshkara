using System;
using Acr.UserDialogs;
using Bshkara.Mobile.iOS.Services;
using Bshkara.Mobile.Localization;
using Bshkara.Mobile.Services;
using Bshkara.Mobile.Services.PlatformService;
using Facebook.CoreKit;
using FFImageLoading.Forms.Touch;
using Foundation;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms;
using XLabs.Forms.Services;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.Media;

namespace Bshkara.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : XFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitFacebook();

            Forms.Init();

            CachedImageRenderer.Init();
            KeyboardOverlapRenderer.Init();

            App.OnContainerSet += RegisterNativeServices;
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void InitFacebook()
        {
            Settings.AppID = "1112060955498040";
            Settings.DisplayName = "Bshkara";
        }

        private void RegisterNativeServices(IDependencyContainer container)
        {
            container.Register(t => AppleDevice.CurrentDevice);
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

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }
    }
}