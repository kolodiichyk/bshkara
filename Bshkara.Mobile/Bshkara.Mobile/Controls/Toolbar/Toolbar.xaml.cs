using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bshkara.Mobile.Controls.Toolbar
{
    public partial class Toolbar : ContentView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(Toolbar), string.Empty);

        public static readonly BindableProperty SubTitleProperty =
            BindableProperty.Create(nameof(SubTitle), typeof(string), typeof(Toolbar), string.Empty);

        public static readonly BindableProperty ShowBottomLineProperty =
            BindableProperty.Create(nameof(ShowBottomLine), typeof(bool), typeof(Toolbar), false);

        public static readonly BindableProperty BottomLineHeightProperty =
            BindableProperty.Create(nameof(BottomLineHeight), typeof(double), typeof(Toolbar), (double) 1);

        public static readonly BindableProperty BottomLineColorProperty =
            BindableProperty.Create(nameof(BottomLineColor), typeof(Color), typeof(Toolbar), Color.Transparent);

        public static readonly BindableProperty ShowDropDownIconProperty =
            BindableProperty.Create(nameof(ShowDropDownIcon), typeof(bool), typeof(Toolbar), false);

        public static readonly BindableProperty TitleTapCommandProperty =
        	BindableProperty.Create(nameof(TitleTapCommand), typeof(ICommand), typeof(Toolbar));

		public static readonly BindableProperty SetIOSTopPaddingProperty =
			BindableProperty.Create(nameof(SetIOSTopPadding), typeof(bool), typeof(Toolbar), true);

        public Toolbar()
        {
            InitializeComponent();

            var rightToolbarItems = new ObservableCollection<View>();
            rightToolbarItems.CollectionChanged += OnRightToolbarItemsCollectionChanged;
            RightToolbarItems = rightToolbarItems;

            var leftToolbarItems = new ObservableCollection<View>();
            leftToolbarItems.CollectionChanged += OnLeftToolbarItemsCollectionChanged;
            LeftToolbarItems = leftToolbarItems;

            var centralToolbarItems = new ObservableCollection<View>();
            centralToolbarItems.CollectionChanged += OnCentralToolbarItemsCollectionChanged;
            CentralToolBarItems = centralToolbarItems;

			ContentGrid.Padding = new Thickness(0, Device.OS == TargetPlatform.iOS ? 20 : 0, 0, 0);

            Title = null;
        }

        public IList<View> RightToolbarItems { get; set; }

        public IList<View> LeftToolbarItems { get; set; }

        public IList<View> CentralToolBarItems { get; set; }

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string SubTitle
        {
            get { return (string) GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }

        public bool ShowBottomLine
        {
            get { return (bool) GetValue(ShowBottomLineProperty); }
            set { SetValue(ShowBottomLineProperty, value); }
        }

        public bool ShowDropDownIcon
        {
            get { return (bool) GetValue(ShowDropDownIconProperty); }
            set { SetValue(ShowDropDownIconProperty, value); }
        }

		public bool SetIOSTopPadding
		{
			get { return (bool)GetValue(SetIOSTopPaddingProperty); }
			set { SetValue(SetIOSTopPaddingProperty, value); }
		}

        public ICommand TitleTapCommand
        {
            get { return (ICommand)GetValue(TitleTapCommandProperty); }
            set { SetValue(TitleTapCommandProperty, value); }
        }

        public double BottomLineHeight
        {
            get { return (double) GetValue(BottomLineHeightProperty); }
            set { SetValue(BottomLineHeightProperty, value); }
        }

        public Color BottomLineColor
        {
            get { return (Color) GetValue(BottomLineColorProperty); }
            set { SetValue(BottomLineColorProperty, value); }
        }

        private void OnRightToolbarItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RightToolBarItemsContainer.Children.Clear();
            foreach (var toolbarItem in RightToolbarItems)
            {
                toolbarItem.BindingContext = BindingContext;
                RightToolBarItemsContainer.Children.Add(toolbarItem);
            }
        }

        private void OnLeftToolbarItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            LeftToolBarItemsContainer.Children.Clear();
            foreach (var toolbarItem in LeftToolbarItems)
            {
                toolbarItem.BindingContext = BindingContext;
                LeftToolBarItemsContainer.Children.Add(toolbarItem);
            }
        }

        private void OnCentralToolbarItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CentralToolBarItemsContainer.Children.Clear();

            if (CentralToolBarItems.Any())
                foreach (var toolbarItem in CentralToolBarItems)
                {
                    toolbarItem.BindingContext = BindingContext;
                    CentralToolBarItemsContainer.Children.Add(toolbarItem);
                }
            else
                OnPropertyChanged(nameof(Title));
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Title))
            {
                TitleLabel.Text = Title;
                TitleLabel.IsVisible = !string.IsNullOrWhiteSpace(Title);
            }

            if (propertyName == nameof(SubTitle))
            {
                SubTitleLabel.Text = SubTitle;
                SubTitleLabel.IsVisible = !string.IsNullOrWhiteSpace(SubTitle);

                if (!SubTitleLabel.IsVisible)
                {
                    TitleLabel.FontAttributes = FontAttributes.Bold;
                    TitleLabel.FontSize = 16;
                }
                else
                {
                    TitleLabel.FontAttributes = FontAttributes.None;
                    TitleLabel.FontSize = 14;
                }
            }

            if (propertyName == nameof(ShowBottomLine))
                BottomLine.IsVisible = ShowBottomLine;

            if (propertyName == nameof(BottomLineHeight))
                BottomLine.HeightRequest = BottomLineHeight;

            if (propertyName == nameof(BottomLineColor))
                BottomLine.Color = BottomLineColor;

            if (propertyName == nameof(ShowDropDownIcon))
                DropDownIcon.IsVisible = ShowDropDownIcon;

			if (propertyName == nameof(SetIOSTopPadding))
				ContentGrid.Padding = new Thickness(0, SetIOSTopPadding && Device.OS == TargetPlatform.iOS ? 20 : 0,0,0);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach (var toolbarItem in LeftToolbarItems)
                toolbarItem.BindingContext = BindingContext;

            foreach (var toolbarItem in RightToolbarItems)
                toolbarItem.BindingContext = BindingContext;

            foreach (var toolbarItem in CentralToolBarItems)
                toolbarItem.BindingContext = BindingContext;
        }

        private void OnTitleTapped(object sender, EventArgs e)
        {
            TitleTapCommand?.Execute(null);
        }
    }
}