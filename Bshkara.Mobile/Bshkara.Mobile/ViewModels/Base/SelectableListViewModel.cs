using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bashkra.ApiClient.Models.Base;
using Bshkara.Mobile.Services.WebService;
using PropertyChanged;

namespace Bshkara.Mobile.ViewModels.Base
{
    public abstract class SelectableListViewModel<T> : BaseListViewModel<ISelectable<T>> where T : ApiBaseModel
    {
        public IList<Guid> SelectedIds = new List<Guid>();

        protected SelectableListViewModel(IWebService webService) : base(webService)
        {
        }

        public override Task ItemTapped(ISelectable<T> item)
        {
            item.IsSelected = !item.IsSelected;

            if (item.IsSelected)
                SelectedIds.Add(item.Item.Id);
            else
                SelectedIds.Remove(item.Item.Id);

            return base.ItemTapped(item);
        }
    }

    public interface ISelectable<T> where T : class
    {
        T Item { get; set; }

        bool IsSelected { get; set; }
    }

    [ImplementPropertyChanged]
    public class Selectable<T> : ISelectable<T> where T : class
    {
        public Selectable()
        {
            IsSelected = false;
        }

        public Selectable(T item)
        {
            Item = item;
        }

        public T Item { get; set; }

        public bool IsSelected { get; set; }
    }
}