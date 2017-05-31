using Xamarin.Forms;

namespace Bshkara.Mobile.Controls.RoundedBoxView
{
    public class RoundedBoxView : BoxView
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof (double), typeof (RoundedBoxView), default(double));

        /// <summary>
        /// Thickness property of border
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create<RoundedBoxView, int>(
                p => p.BorderThickness, 0);

        /// <summary>
        /// Color property of border
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create<RoundedBoxView, Color>(
                p => p.BorderColor, Color.White);

        public double CornerRadius
        {
            get { return (double) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Border thickness of circle image
        /// </summary>
        public int BorderThickness
        {
            get { return (int) GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        /// <summary>
        /// Border Color of circle image
        /// </summary>
        public Color BorderColor
        {
            get { return (Color) GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
    }
}