using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls
{
    public class ExtendedListView : ListView
    {
        public static readonly BindableProperty IsHorizontalProperty =
            BindableProperty.Create(nameof(IsHorizontal), typeof(bool), typeof(ExtendedListView), false);

        public static readonly BindableProperty ItemsPerPageProperty =
            BindableProperty.Create(nameof(ItemsPerPage), typeof(int), typeof(ExtendedListView), 25);

        public static readonly BindableProperty LoadMoreCommandProperty =
            BindableProperty.Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(ExtendedListView),
                default(ICommand));

        public static readonly BindableProperty RowColorProperty =
            BindableProperty.Create(nameof(RowColor), typeof(Color), typeof(ExtendedListView), Color.Transparent);

        public static readonly BindableProperty OddRowColorProperty =
            BindableProperty.Create(nameof(OddRowColor), typeof(Color), typeof(ExtendedListView), Color.Transparent);

        private double _height;
        private double _width;

        public ExtendedListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            ItemAppearing += InfiniteListViewItemAppearing;

            ItemSelected += delegate(object sender, SelectedItemChangedEventArgs args)
            {
                if (args == null)
                    return;

                // Clear selection
                ((ExtendedListView) sender).SelectedItem = null;
            };

            //SizeChanged += OnSizeChanged;
        }

        public ICommand LoadMoreCommand
        {
            get { return (ICommand) GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public int ItemsPerPage
        {
            get { return (int) GetValue(ItemsPerPageProperty); }
            set { SetValue(ItemsPerPageProperty, value); }
        }

        public Color RowColor
        {
            get { return (Color) GetValue(RowColorProperty); }
            set { SetValue(RowColorProperty, value); }
        }

        public Color OddRowColor
        {
            get { return (Color) GetValue(OddRowColorProperty); }
            set { SetValue(OddRowColorProperty, value); }
        }

        public bool IsHorizontal
        {
            get { return (bool) GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }

        protected override void SetupContent(Cell pContent, int pIndex)
        {
            base.SetupContent(pContent, pIndex);
            var currentViewCell = pContent as ViewCell;

            if (currentViewCell != null)
                currentViewCell.View.BackgroundColor = pIndex%2 == 0
                    ? RowColor
                    : OddRowColor;
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs)
        {
            SetOrientation();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsHorizontal))
                SetOrientation();
        }

        private void SetOrientation()
        {
            _height = Height;
            _width = Width;
            if (IsHorizontal)
                SetHorizontal();
            else
                SetVertical();
        }

        private void SetHorizontal()
        {
            Rotation = 270;
            WidthRequest = _height;
            HeightRequest = _width;
        }

        private void SetVertical()
        {
            Rotation = 0;
            WidthRequest = _width;
            HeightRequest = _height;
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            child.BindingContext = null;
        }

        private void InfiniteListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            if ((items != null) && (items.Count >= ItemsPerPage) && (items.Count - 3 >= 0) &&
                (e.Item == items[items.Count - 3]))
                if ((LoadMoreCommand != null) && LoadMoreCommand.CanExecute(null))
                    LoadMoreCommand.Execute(null);
        }
    }
}