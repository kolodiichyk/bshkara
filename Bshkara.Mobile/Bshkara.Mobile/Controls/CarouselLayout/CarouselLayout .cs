using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls.CarouselLayout
{
    public class CarouselLayout : ScrollView
    {
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(CarouselLayout),
                0,
                BindingMode.TwoWay,
                propertyChanged:
                async (bindable, oldValue, newValue) => { await ((CarouselLayout) bindable).UpdateSelectedItem(); }
            );

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(CarouselLayout),
                null,
                propertyChanged:
                (bindableObject, oldValue, newValue) => { ((CarouselLayout) bindableObject).ItemsSourceChanged(); }
            );

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(CarouselLayout),
                null,
                BindingMode.TwoWay,
                propertyChanged:
                (bindable, oldValue, newValue) => { ((CarouselLayout) bindable).UpdateSelectedIndex(); }
            );

        private readonly StackLayout _stack;

        private bool _layingOutChildren;

        private int _selectedIndex;

        public CarouselLayout()
        {
            Orientation = ScrollOrientation.Horizontal;

            _stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            Content = _stack;
        }

        public bool IsLocked { get; set; }

        public Func<bool> IsKeyboard { get; set; }

        [DoNotNotify]
        public IList<View> Children
        {
            get { return _stack.Children; }
        }

        [DoNotNotify]
        public int SelectedIndex
        {
            get { return (int) GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        [DoNotNotify]
        public IList ItemsSource
        {
            get { return (IList) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate { get; set; }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if (_layingOutChildren) return;

            _layingOutChildren = true;
            foreach (var child in Children) child.WidthRequest = width;
            _layingOutChildren = false;
        }

        private async Task UpdateSelectedItem()
        {
            try
            {
                await Task.Delay(300);
                SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
            }
            catch (Exception eX)
            {
                Debug.WriteLine(eX.Message);
                //do nothing
            }
        }

        private void ItemsSourceChanged()
        {
            _stack.Children.Clear();
            foreach (var item in ItemsSource)
            {
                var view = (View) ItemTemplate.CreateContent();
                var bindableObject = view as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = item;
                _stack.Children.Add(view);
            }

            if (_selectedIndex >= 0)
                SelectedIndex = _selectedIndex;

            if (SelectedItem != null)
                UpdateSelectedIndex();
        }

        private void UpdateSelectedIndex()
        {
            if (SelectedItem == null) return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);
        }
    }
}