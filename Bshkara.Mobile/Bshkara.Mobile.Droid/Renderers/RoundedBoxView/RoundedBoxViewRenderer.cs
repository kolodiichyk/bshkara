using System.ComponentModel;
using Bshkara.Mobile.Controls.RoundedBoxView;
using Bshkara.Mobile.Droid.Renderers.RoundedBoxView;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly:
    ExportRenderer(typeof (RoundedBoxView), typeof (RoundedBoxViewRenderer))]

namespace Bshkara.Mobile.Droid.Renderers.RoundedBoxView
{
    public class RoundedBoxViewRenderer : ViewRenderer<Controls.RoundedBoxView.RoundedBoxView, View>
    {
        private Controls.RoundedBoxView.RoundedBoxView _formControl
        {
            get { return Element; }
        }

        public static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.RoundedBoxView.RoundedBoxView> e)
        {
            base.OnElementChanged(e);

            this.InitializeFrom(_formControl);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            this.UpdateFrom(_formControl, e.PropertyName);
        }
    }
}