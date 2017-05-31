using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bshkara.Mobile.Services.WebService;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class AgenciesViewModel : ViewModelWithWebService
    {
        public AgenciesViewModel(IWebService webService) : base(webService)
        {
            MessagingCenter.Subscribe<AgenciesFilterViewModel>(this,
                AgenciesFilterViewModel.MessageKeyAgenciesFilterChanged,
                OnFilterChanged);

            Task.Run(async () =>
            {
                State = AgenciesPageState.Wait;
                await GetData();
            });
        }

        public ApiAgency SelectedAgency { get; set; }

        public bool IsRefreshing { get; set; }

        public ICommand PullToRefreshCommand => new Command(PullToRefreshAsync);

        public ICommand RefreshCommand => new Command(RefreshAsync);

        public AgenciesPageState State { get; set; }

        public ObservableCollection<ApiAgency> Agencies { get; set; } = new ObservableCollection<ApiAgency>();

        public ICommand ShowFilterCommand => new Command(ShowFilter);

        private void OnFilterChanged(AgenciesFilterViewModel obj)
        {
        }

        private void ShowFilter()
        {
        }

        private void RefreshAsync()
        {
            OnFilterChanged(null);
        }

        public async Task GetData()
        {
            Agencies = new ObservableCollection<ApiAgency>(await WebService.GetAgenciesAsync(new AgenciesArgs()));
            State = Agencies.Any() ? AgenciesPageState.List : AgenciesPageState.NoRecords;
        }

        private async void PullToRefreshAsync()
        {
            IsRefreshing = true;
            await Task.Run(async () => await GetData());
            IsRefreshing = false;
        }
    }

    public enum AgenciesPageState
    {
        List,
        Wait,
        NoRecords
    }
}