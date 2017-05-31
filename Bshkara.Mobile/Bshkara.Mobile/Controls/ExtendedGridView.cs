using Xamarin.Forms;
using XLabs;
using XLabs.Forms.Controls;

namespace Bshkara.Mobile.Controls
{
    public class ExtendedGridView : GridView
    {
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof (object), typeof (ExtendedGridView), null,
                BindingMode.TwoWay);

        public ExtendedGridView()
        {
            ItemSelected += OnItemSelected;
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private void OnItemSelected(object sender, EventArgs<object> eventArgs)
        {
            SelectedItem = eventArgs.Value;

            // Clear selection
            SelectedItem = null;
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
            if (propertyName == nameof(ItemsSource))
            {
                OnParentSet();
            }
        }
    }
}