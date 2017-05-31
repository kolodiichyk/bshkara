using System.ComponentModel;
using Bshkara.Mobile.Controls.RoundedBoxView;
using Bshkara.Mobile.iOS.Renderers.RoundedBoxView;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:
    ExportRenderer(typeof (RoundedBoxView), typeof (RoundedBoxViewRenderer))]

namespace Bshkara.Mobile.iOS.Renderers.RoundedBoxView
{
    [Preserve(AllMembers = true)]
    public class RoundedBoxViewRenderer : BoxRenderer
    {
        private Controls.RoundedBoxView.RoundedBoxView _formControl
        {
            get { return Element as Controls.RoundedBoxView.RoundedBoxView; }
        }

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
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