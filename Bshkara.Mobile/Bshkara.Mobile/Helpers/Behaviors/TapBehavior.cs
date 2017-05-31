using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Bshkara.Mobile.Helpers.Behaviors
{
    public class TapBehavior : Behavior<ExtendedLabel>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof (ICommand), typeof (TapBehavior), null,
                BindingMode.TwoWay);

        private ExtendedLabel _label;

        private ICommand _tapGestureCommand => new Command(TapGesture);

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ExtendedLabel bindable)
        {
            base.OnAttachedTo(bindable);
            _label = bindable;

            var tapGestureRecognizer = new TapGestureRecognizer {Command = _tapGestureCommand};
            _label.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void TapGesture()
        {
            _label.FadeTo(0.5).ContinueWith(task => _label.FadeTo(1));

            if (Command != null)
            {
                Command.Execute(null);
            }
        }
    }
}