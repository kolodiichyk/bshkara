using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Bashkra.ApiClient.Requests;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.ViewModels.Base;
using Bshkara.Mobile.Views;
using Bshkara.Mobile.Views.AgenciesView;
using Bshkara.Mobile.Views.HomeView;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using MenuItem = Bshkara.Mobile.Helpers.MenuItem;

namespace Bshkara.Mobile.ViewModels
{
    public class MenuViewModel : ViewModel
    {
        public MenuViewModel()
        {
            InitMenuItems();
        }

        public ObservableCollection<ISelectable<MenuItem>> MenuItems { get; set; }

        public ISelectable<MenuItem> SelectedItem { get; set; }

        public int SelectedIndex { get; set; }

        public ICommand LogoutCommand => new Command(() => MenuNavigation<LoginViewModel, LoginView>(() =>
        {
            Settings.AccessToken = null;
            Settings.User = null;
            Settings.MaidFilter = new MaidsArgs();
        }, App.DisableMenu));

        private void MenuNavigation<TViewModel, TPage>(Action actionBefore = null, Action actionAfter = null)
            where TViewModel : ViewModel
        {
            actionBefore?.Invoke();

            App.SetMainPage((Page) ViewFactory.CreatePage<TViewModel, TPage>());
            var masterDetailPage = Application.Current.MainPage as MasterDetailPage;
            if (masterDetailPage != null)
                masterDetailPage.IsPresented = false;

            actionAfter?.Invoke();
        }

        protected override void NotifyPropertyChanged(string propertyName = null)
        {
            base.NotifyPropertyChanged(propertyName);
            if (propertyName == nameof(SelectedItem))
            {
                var oldItem = MenuItems[SelectedIndex];
                if (oldItem != null)
                    oldItem.IsSelected = false;

                SelectedIndex = MenuItems.IndexOf(SelectedItem);
                SelectedItem.IsSelected = true;
                SelectedItem.Item.Action?.Invoke();
            }
        }

        private void InitMenuItems()
        {
            MenuItems = new ObservableCollection<ISelectable<MenuItem>>
            {
                new Selectable<MenuItem>
                {
                    Item = new MenuItem
                    {
                        Index = 1,
                        Name = "Maids",
                        Action = () => { MenuNavigation<HomeViewModel, HomeView>(); }
                    },
                    IsSelected = true
                },
                new Selectable<MenuItem>
                {
                    Item = new MenuItem
                    {
                        Index = 2,
                        Name = "Agencies",
                        Action = () => { MenuNavigation<AgenciesViewModel, AgenciesView>(); }
                    }
                }
            };
        }
    }
}