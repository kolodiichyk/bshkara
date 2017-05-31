using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bashkra.ApiClient.Models;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.Services.WebService;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class HomeViewModel : BaseHomeViewModel
    {
        private readonly IWebService _webService;
        protected int CurentPage;

        public HomeViewModel(IWebService webService)
        {
            _webService = webService;
            CurentPage = 0;
            ItemsPerPage = 10;

            MessagingCenter.Subscribe<FilterViewModel>(this, FilterViewModel.MessageKeyMaidFilterChanged,
                OnFilterChanged);

            Task.Run(async () =>
            {
                HomePageState = HomePageState.Wait;
                Maids = (await GetItemsSource(CurentPage))?.ToObservableCollection();
                HomePageState = Maids.Any() ? HomePageState.List : HomePageState.NoRecords;
            });
        }

        public int ItemsPerPage { get; set; }

        public HomePageState HomePageState { get; set; }

        public ObservableCollection<ApiMaid> Maids { get; set; }

        public ApiMaid SelectedMaid { get; set; }

        public bool IsRefreshing { get; set; }

        public ICommand ShowListCommand => new Command(ShowList);

        public ICommand ShowGridCommand => new Command(ShowGrid);

        public ICommand PullToRefreshCommand => new Command(PullRefreshAsync);

        public ICommand RefreshCommand => new Command(RefreshAsync);

        public ICommand ShowFilterCommand => new Command(ShowFilteAsync);

        public ICommand LoadMoreCommand => new Command(async () =>
        {
            IsBusy = true;
            CurentPage++;
            var items = (await GetItemsSource(CurentPage))?.ToObservableCollection();
            if (items == null || !items.Any())
            {
                CurentPage--;
                IsBusy = false;
                return;
            }

            foreach (var item in items)
                Maids.Add(item);

            IsBusy = false;
        });

        protected override void OnCitiesSelect(CitiesViewModel sender, ApiCity city)
        {
            base.OnCitiesSelect(sender, city);

            var filter = Settings.MaidFilter;
            filter.City = City.Id;
            Settings.MaidFilter = filter;

            OnFilterChanged(null);
        }

        private void RefreshAsync()
        {
            OnFilterChanged(null);
        }

        private void OnFilterChanged(FilterViewModel obj)
        {
            Task.Run(async () =>
            {
                HomePageState = HomePageState.Wait;
                CurentPage = 0;
                Maids = new ObservableCollection<ApiMaid>(await GetItemsSource(CurentPage));
                HomePageState = Maids.Any() ? HomePageState.List : HomePageState.NoRecords;
            });
        }

        private async void ShowFilteAsync()
        {
            await Navigation.PushModalAsync<FilterViewModel>();
        }

        private async void PullRefreshAsync()
        {
            IsRefreshing = true;
            CurentPage = 0;
            Maids = (await GetItemsSource(CurentPage))?.ToObservableCollection();
            IsRefreshing = false;
        }

        protected async Task<IEnumerable<ApiMaid>> GetItemsSource(int page)
        {
            var filter = Settings.MaidFilter;
            filter.Paging.PageNumber = page;
            filter.Paging.PageSize = ItemsPerPage;
            return await _webService.GetMaidsAsync(filter);
        }

        private void ShowList()
        {
            if (HomePageState != HomePageState.NoRecords)
                HomePageState = HomePageState.List;
        }

        private void ShowGrid()
        {
            if (HomePageState != HomePageState.NoRecords)
                HomePageState = HomePageState.Grid;
        }

        protected override async void NotifyPropertyChanged(string propertyName = null)
        {
            base.NotifyPropertyChanged(propertyName);
            if (propertyName == nameof(SelectedMaid) && SelectedMaid != null)
                await Navigation.For<MaidDetailsViewModel>().WithParam(t => t.Maid, SelectedMaid).Navigate();
        }
    }

    public enum HomePageState
    {
        List,
        Grid,
        Wait,
        NoRecords
    }
}