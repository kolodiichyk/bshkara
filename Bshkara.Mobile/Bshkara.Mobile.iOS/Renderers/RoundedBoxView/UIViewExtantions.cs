using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Bshkara.Mobile.iOS.Renderers.RoundedBoxView
{
    public static class UIViewExtensions
    {
        public static void InitializeFrom(this UIView nativeControl, Controls.RoundedBoxView.RoundedBoxView formsControl)
        {
            if (nativeControl == null || formsControl == null)
                return;

            nativeControl.Layer.MasksToBounds = true;
            nativeControl.Layer.CornerRadius = (float) formsControl.CornerRadius;
            nativeControl.UpdateBorder(formsControl.BorderColor, formsControl.BorderThickness);
        }

        public static void UpdateFrom(this UIView nativeControl, Controls.RoundedBoxView.RoundedBoxView formsControl,
            string propertyChanged)
        {
            if (nativeControl == null || formsControl == null)
                return;

            if (propertyChanged == Controls.RoundedBoxView.RoundedBoxView.CornerRadiusProperty.PropertyName)
            {
                nativeControl.Layer.CornerRadius = (float) formsControl.CornerRadius;
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

        public static void UpdateBorder(this UIView nativeControl, Color color, int thickness)
        {
            nativeControl.Layer.BorderColor = color.ToCGColor();
            nativeControl.Layer.BorderWidth = thickness;
        }
    }
}