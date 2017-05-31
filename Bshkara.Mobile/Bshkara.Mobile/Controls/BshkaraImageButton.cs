using System.Windows.Input;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls
{
    public class BshkaraImageButton : CachedImage
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof (ICommand), typeof (FontAwesomeIcon.FontAwesomeIcon), null,
                BindingMode.TwoWay);

        public BshkaraImageButton()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Command = _tapGestureCommand;
            GestureRecognizers.Add(tapGestureRecognizer);
        }

        private ICommand _tapGestureCommand => new Command(TapGesture);

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
    }
}