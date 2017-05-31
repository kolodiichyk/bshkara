using Xamarin.Forms;

namespace Bshkara.Mobile.Controls.BashkraEntry
{
    public partial class BashkraEntry : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(BashkraEntry), string.Empty);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(BashkraEntry), string.Empty);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(BashkraEntry), false);

        public BashkraEntry()
        {
            InitializeComponent();

            InnerEntry.Focused += InnerEntryOnFocused;
            InnerEntry.Unfocused += InnerEntryOnFocused;

            GestureRecognizers.Add(new TapGestureRecognizer {Command = new Command(() => InnerEntry.Focus())});
        }

        public bool IsPassword
        {
            get { return (bool) GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Placeholder
        {
            get { return (string) GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        private void InnerEntryOnFocused(object sender, FocusEventArgs focusEventArgs)
        {
            InnerBoxView.BackgroundColor = focusEventArgs.IsFocused
                ? (Color) Application.Current.Resources["LightAccent"]
                : (Color) Application.Current.Resources["White"];
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (nameof(Text) == propertyName)
                InnerEntry.Text = Text;

            if (nameof(Placeholder) == propertyName)
                InnerEntry.Placeholder = Placeholder;

            if (nameof(IsPassword) == propertyName)
                InnerEntry.IsPassword = IsPassword;
        }
    }
}