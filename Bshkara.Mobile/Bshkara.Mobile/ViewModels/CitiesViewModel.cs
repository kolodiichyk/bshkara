using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.Services.WebService;
using Bshkara.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class CitiesViewModel : BaseListViewModel<ApiCity>
    {
        public const string MessageForFilterKey = "City";

        public const string MessageForUserLocationKey = "UserLocation";

        private readonly CitiesArgs _citiesArgs;

        public string MessageKey { get; set; }

        public CitiesViewModel(IWebService webService) : base(webService)
        {
            Title = "Select city";
			State = Helpers.WaitPageState.Wait;
            _citiesArgs = new CitiesArgs();
        }

        protected override async Task<IEnumerable<ApiCity>> GetItemsSource(int page)
        {
            _citiesArgs.Paging.PageNumber = page;
            return
                (await WebService.GetCitiesAsync(_citiesArgs))?.ToObservableCollection();
        }

        public override void Reset()
        {
            MessagingCenter.Send<CitiesViewModel, ApiCity>(this, MessageKey, null);
            base.Reset();
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Search))
            {
                CurentPage = 0;
                _citiesArgs.Search = Search;
                Items = (await GetItemsSource(CurentPage))?.ToObservableCollection();
            }
        }

        public override Task ItemTapped(ApiCity item)
        {
            MessagingCenter.Send(this, MessageKey, item);
            CloseCommand.Execute(null);
            return base.ItemTapped(item);
        }
    }
}