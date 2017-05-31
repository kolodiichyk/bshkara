using Android.Graphics.Drawables;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

namespace Bshkara.Mobile.Droid.Renderers.RoundedBoxView
{
    public static class UIViewExtensions
    {
        public static void InitializeFrom(this View nativeControl, Controls.RoundedBoxView.RoundedBoxView formsControl)
        {
            if (nativeControl == null || formsControl == null)
                return;

            var background = new GradientDrawable();

            background.SetColor(formsControl.BackgroundColor.ToAndroid());

            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
            {
                nativeControl.Background = background;
            }
            else
            {
                nativeControl.SetBackgroundDrawable(background);
            }

            nativeControl.UpdateCornerRadius(formsControl.CornerRadius);
            nativeControl.UpdateBorder(formsControl.BorderColor, formsControl.BorderThickness);
        }

        public static void UpdateFrom(this View nativeControl, Controls.RoundedBoxView.RoundedBoxView formsControl,
            string propertyChanged)
        {
            if (nativeControl == null || formsControl == null)
                return;

            if (propertyChanged == Controls.RoundedBoxView.RoundedBoxView.CornerRadiusProperty.PropertyName)
            {
                nativeControl.UpdateCornerRadius(formsControl.CornerRadius);
            }
            if (propertyChanged == VisualElement.BackgroundColorProperty.PropertyName)
            {
                var background = nativeControl.Background as GradientDrawable;

                if (background != null)
                {
                    background.SetColor(formsControl.BackgroundColor.ToAndroid());
                }
                else
                {
                    var grad = new GradientDrawable();
                    grad.SetOrientation(GradientDrawable.Orientation.TopBottom);
                    grad.SetColors(new[]
                    {
                        formsControl.BackgroundColor.ToAndroid().ToArgb(),
                        formsControl.BackgroundColor.ToAndroid().ToArgb()
                    });

                    nativeControl.Background = grad;
                }

                nativeControl.UpdateCornerRadius(formsControl.CornerRadius);
                nativeControl.UpdateBorder(formsControl.BorderColor, formsControl.BorderThickness);
            }

            if (propertyChanged == Controls.RoundedBoxView.RoundedBoxView.BorderColorProperty.PropertyName)
            {
                nativeControl.UpdateBorder(formsControl.BorderColor, formsControl.BorderThickness);
            }

            if (propertyChanged == Controls.RoundedBoxView.RoundedBoxView.BorderThicknessProperty.PropertyName)
            {
                nativeControl.UpdateBorder(formsControl.BorderColor, formsControl.BorderThickness);
            }
        }

        public static void UpdateBorder(this View nativeControl, Color color, int thickness)
        {
            var backgroundGradient = nativeControl.Background as GradientDrawable;

            if (backgroundGradient != null)
            {
                var relativeBorderThickness = thickness;
                backgroundGradient.SetStroke(relativeBorderThickness, color.ToAndroid());
            }
        }

        public static void UpdateCornerRadius(this View nativeControl, double cornerRadius)
        {
            var backgroundGradient = nativeControl.Background as GradientDrawable;

            if (backgroundGradient != null)
            {
                var relativeCornerRadius = (float) (cornerRadius*3.7);
                backgroundGradient.SetCornerRadius(relativeCornerRadius);
            }
        }
    }
}