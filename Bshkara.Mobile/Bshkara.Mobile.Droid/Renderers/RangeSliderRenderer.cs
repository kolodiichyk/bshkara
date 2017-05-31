using Android.Graphics;
using Bshkara.Mobile.Controls;
using Bshkara.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.RangeSlider.Forms;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof (BshkaraRangeSlider), typeof (CustomRangeSliderRenderer))]

namespace Bshkara.Mobile.Droid.Renderers
{
    public class CustomRangeSliderRenderer : RangeSliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RangeSlider> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            if (Control != null)
            {
                Control.ThumbImage = BitmapFactory.DecodeResource(Resources, Resource.Drawable.bshkara_seek_thumb_normal);
                Control.ThumbPressedImage = BitmapFactory.DecodeResource(Resources,
                    Resource.Drawable.bshkara_seek_thumb_pressed);
                Control.ThumbDisabledImage = BitmapFactory.DecodeResource(Resources,
                    Resource.Drawable.bshkara_seek_thumb_normal);

                Control.ActiveColor = Color.FromHex("#721DB1").ToAndroid();
                Control.DefaultColor = Color.FromHex("#E3E3E3").ToAndroid();
            }
        }
    }
}