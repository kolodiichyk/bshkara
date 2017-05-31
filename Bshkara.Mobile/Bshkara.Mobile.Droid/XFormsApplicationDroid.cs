using System;
using Android.Speech.Tts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services.Email;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services.IO;
using XLabs.Platform.Services.Media;

namespace Bshkara.Mobile.Droid
{
    public class XFormsApplicationAppCompatDroid : FormsAppCompatActivity
    {
        public EventHandler<EventArgs> Destroy { get; set; }

        public EventHandler<EventArgs> Pause { get; set; }

        public EventHandler<EventArgs> Restart { get; set; }

        public EventHandler<EventArgs> Resume { get; set; }

        public EventHandler<EventArgs> Start { get; set; }

        public EventHandler<EventArgs> Stop { get; set; }

        protected override void OnDestroy()
        {
            Destroy?.Invoke(this, new EventArgs());
            base.OnDestroy();
        }

        protected override void OnPause()
        {
            Pause?.Invoke(this, new EventArgs());
            base.OnPause();
        }

        protected override void OnRestart()
        {
            Restart?.Invoke(this, new EventArgs());
            base.OnRestart();
        }

        protected override void OnResume()
        {
            Resume?.Invoke(this, new EventArgs());
            base.OnResume();
        }

        protected override void OnStart()
        {
            Start?.Invoke(this, new EventArgs());
            base.OnStart();
        }

        protected override void OnStop()
        {
            var handler = Stop;
            if (handler != null)
                handler(this, new EventArgs());

            base.OnStop();
        }
    }

    /// <summary>
    ///     Class XFormsAppDroid.
    /// </summary>
    public class XFormsAppDroid : XFormsApp<XFormsApplicationAppCompatDroid>
    {
        public XFormsAppDroid()
        {
        }

        public XFormsAppDroid(XFormsApplicationAppCompatDroid app) : base(app)
        {
        }

        public void RaiseBackPress()
        {
            OnBackPress();
        }

        protected override void OnInit(XFormsApplicationAppCompatDroid app, bool initServices = true)
        {
            AppContext.Start += (o, e) => OnStartup();
            AppContext.Stop += (o, e) => OnClosing();
            AppContext.Pause += (o, e) => OnSuspended();
            AppContext.Resume += (o, e) => OnResumed();
            //AppDataDirectory = Environment.ExternalStorageDirectory.AbsolutePath;

            if (initServices)
            {
                DependencyService.Register<TextToSpeechService>();
                DependencyService.Register<Geolocator>();
                DependencyService.Register<MediaPicker>();
                DependencyService.Register<SoundService>();
                DependencyService.Register<EmailService>();
                DependencyService.Register<FileManager>();
                DependencyService.Register<AndroidDevice>();
            }

            base.OnInit(app);
        }
    }
}