using System.Collections;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls.CarouselLayout
{
    public partial class CarouselIndicator
    {
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(int), typeof(CarouselIndicator), default(int), propertyChanged:
            (bindable, value, newValue) => { ((CarouselIndicator) bindable).SelectPage((int) newValue); });

        public static readonly BindableProperty TemplateProperty = BindableProperty.Create(nameof(Template),
            typeof(DataTemplate), typeof(CarouselIndicator), default(DataTemplate));

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource),
            typeof(IList), typeof(CarouselIndicator), propertyChanged:
            (bindable, value, newValue) =>
            {
                ((CarouselIndicator) bindable).PopulateListFromItemSource((IEnumerable) newValue);
            });

        public CarouselIndicator()
        {
            InitializeComponent();

            ItemCount = GridStack.Children.Count;
        }

        public int ItemCount { get; set; }

        public int SelectedItem
        {
            get { return (int) GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                SelectPage(value);
            }
        }

        public DataTemplate Template
        {
            get { return (DataTemplate) GetValue(TemplateProperty); }
            set { SetValue(TemplateProperty, value); }
        }

        public IList ItemSource
        {
            get { return (IList) GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        private void PopulateListFromItemSource(IEnumerable source)
        {
            if (source == null) return;

            var listOfItems = source;

            GridStack.Children.Clear();

            GridStack.ColumnDefinitions.Clear();

            foreach (var boundObject in listOfItems)
                GridStack.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});

            var i = 0;
            if (Template == null)
                return;

            foreach (var boundObject in (IList) listOfItems)
            {
                var ui = (View) Template.CreateContent();
                ui.BindingContext = boundObject;
                ui.Parent = this;
                HookGesture(ui);
                GridStack.Children.Add(ui, i, 0);
                i++;
            }
            //initial
            SelectPage(0);
        }

        private void HookGesture(View ui)
        {
            ui.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(o => { })
            });

            ui.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(o =>
                {
                    SelectedItem = GridStack.Children.IndexOf(ui);

                    /*var cii = ui as CarouselImageIndicator;
                    cii?.TapCommand?.Execute(ui.BindingContext);*/
                })
            });
        }

        private void SelectPage(int value)
        {
            if ((value < 0) || (ItemSource == null) || (ItemSource.Count - 1 < value))
                return;

            var newActiveItem = ItemSource?[value] as IHeaderModel;
            if (newActiveItem != null)
            {
                newActiveItem.IsActive = true;

                foreach (var item in ItemSource)
                    if (item != ItemSource[value])
                        (item as IHeaderModel).IsActive = false;
            }
        }
    }
}