using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.Services.WebService;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels.Base
{
    public abstract class BaseListViewModel<T> : BaseViewModel where T : class
    {
        protected int CurentPage;

        protected BaseListViewModel(IWebService webService)
        {
            WebService = webService;
            CurentPage = 0;
        }

        public WaitPageState State { get; set; }

        public string Search { get; set; }

        public bool HideAnyButton { get; set; }

        protected IWebService WebService { get; private set; }

        public ObservableCollection<T> Items { get; set; }

        public ICommand ResetCommand => new Command(Reset);

        public ICommand LoadMoreCommand => new Command(async () =>
        {
            IsBusy = true;
            CurentPage++;
            var items = (await GetItemsSource(CurentPage))?.ToObservableCollection();
            if ((items == null) || !items.Any())
            {
                CurentPage--;
                IsBusy = false;
                return;
            }

            foreach (var item in items)
                Items.Add(item);

            IsBusy = false;
        });

        public T SelectedItem
        {
            get { return null; }
            set
            {
                if (value != null)
                    ItemTapped(value);
            }
        }

        public virtual void Reset()
        {
            CloseCommand.Execute(null);
        }

        public override async void OnViewAppearing()
        {
            State = WaitPageState.Wait;
            Items = (await GetItemsSource(CurentPage))?.ToObservableCollection();
            State = WaitPageState.Content;
            base.OnViewAppearing();
        }

        protected abstract Task<IEnumerable<T>> GetItemsSource(int page);

        public virtual Task ItemTapped(T item)
        {
            return Task.FromResult<object>(null);
        }
    }
}