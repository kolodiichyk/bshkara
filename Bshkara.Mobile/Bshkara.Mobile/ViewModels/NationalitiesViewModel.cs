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
    public class NationalitiesViewModel : BaseListViewModel<ApiNationality>
    {
        public const string MessageKey = "Nationality";

        private readonly NationalitiesArgs _nationalitiesArgs;

        public NationalitiesViewModel(IWebService webService) : base(webService)
        {
            Title = "Select nationality";
			State = Helpers.WaitPageState.Wait;
            _nationalitiesArgs = new NationalitiesArgs();
        }

        public override void Reset()
        {
            MessagingCenter.Send<NationalitiesViewModel, ApiNationality>(this, MessageKey, null);
            base.Reset();
        }

        protected override async Task<IEnumerable<ApiNationality>> GetItemsSource(int page)
        {
            _nationalitiesArgs.Paging.PageNumber = page;
            return
                (await WebService.GetNationalitiesAsync(_nationalitiesArgs))?.ToObservableCollection();
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(Search))
            {
                CurentPage = 0;
                _nationalitiesArgs.Search = Search;
                Items = (await GetItemsSource(CurentPage))?.ToObservableCollection();
            }
        }

        public override Task ItemTapped(ApiNationality item)
        {
            MessagingCenter.Send(this, MessageKey, item);
            CloseCommand.Execute(null);
            return base.ItemTapped(item);
        }
    }
}