using System.Windows.Input;
using Bshkara.Mobile.Controls.Iconize;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Bshkara.Mobile.Controls.FontAwesomeIcon
{
    public class FontAwesomeIcon : ExtendedLabel
    {
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof (string), typeof (FontAwesomeIcon), default(string));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof (ICommand), typeof (FontAwesomeIcon), null,
                BindingMode.TwoWay);

        public FontAwesomeIcon()
        {
			FontFamily = "fontawesome";
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Command = _tapGestureCommand;
            GestureRecognizers.Add(tapGestureRecognizer);
        }

        private ICommand _tapGestureCommand => new Command(TapGesture);

        public string Icon
        {
            get { return (string) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private void TapGesture()
        {
            this.FadeTo(0.5).ContinueWith(task => this.FadeTo(1));
            Command?.Execute(null);
        }

        /// <summary>
        /// Method that is called when a bound property is changed.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        /// <param name="propertyName">
        /// The name of the bound property that changed.
        /// </param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Icon) && !string.IsNullOrWhiteSpace(Icon))
            {
                char fontAwesomeChar;
                if (FontAwesomeCollection.Icons.TryGetValue(Icon, out fontAwesomeChar))
                {
                    Text = fontAwesomeChar.ToString();
                }
            }
        }
    }
}