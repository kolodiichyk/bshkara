using System.Collections.Generic;
using System.Linq;
using Bashkra.ApiClient.Models;
using Bshkara.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls
{
    public class Langueges : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable<ISelectable<ApiLanguage>>), typeof(Skills), null, BindingMode.TwoWay);

        public Langueges()
        {
            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Spacing = 5;
        }

        public IEnumerable<ISelectable<ApiLanguage>> ItemsSource
        {
            get { return (IEnumerable<ISelectable<ApiLanguage>>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        private void CreateLangueges()
        {
            Children.Clear();
            foreach (var item in ItemsSource.OrderBy(t => t.Item.Name))
            {
                var cb = new BshkaraCheckbox {BindingContext = item};
                cb.TextColor = (Color) Application.Current.Resources["DarkGray"];
                cb.SetBinding(BshkaraCheckbox.IsCheckedProperty, "IsSelected", BindingMode.TwoWay);
                cb.SetBinding(BshkaraCheckbox.TextProperty, "Item.Name");

                Children.Add(cb);
            }
        }


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if ((propertyName == ItemsSourceProperty.PropertyName) && (ItemsSource != null))
                CreateLangueges();
        }
    }
}