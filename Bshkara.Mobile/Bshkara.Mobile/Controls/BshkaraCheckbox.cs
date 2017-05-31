using FFImageLoading.Forms;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Bshkara.Mobile.Controls
{
    public class BshkaraCheckbox : StackLayout
    {
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(BshkaraCheckbox), false,
                BindingMode.TwoWay);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(BshkaraCheckbox), string.Empty);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BshkaraCheckbox), Color.Black);

        private readonly CachedImage _image = new CachedImage();

        private readonly ExtendedLabel _label = new ExtendedLabel();

        public BshkaraCheckbox()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 5;
            var tap = new TapGestureRecognizer {Command = new Command(Check)};
            GestureRecognizers.Add(tap);
            _image.Source = "unchecked.png";
            _image.WidthRequest = _image.HeightRequest = 20;
            _image.Aspect = Aspect.AspectFit;
            _image.HorizontalOptions = LayoutOptions.StartAndExpand;
            _image.VerticalOptions = LayoutOptions.CenterAndExpand;

            _label.TextColor = Color.Black;
            _label.Text = "BshkaraCheckbox";
            _label.HorizontalOptions = LayoutOptions.StartAndExpand;
            _label.VerticalOptions = LayoutOptions.Center;
            _label.VerticalTextAlignment = TextAlignment.Center;
            _label.HorizontalTextAlignment = TextAlignment.Start;


            Children.Add(_image);
            Children.Add(_label);
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color) GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool) GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        private void Check()
        {
            IsChecked = !IsChecked;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsChecked))
                _image.Source = IsChecked ? "checked.png" : "unchecked.png";

            if (propertyName == nameof(TextColor))
                _label.TextColor = TextColor;

            if (propertyName == nameof(Text))
                _label.Text = Text;
        }
    }
}