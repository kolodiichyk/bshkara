using System.Web.Mvc;
using Bshkara.Web.Helpers;
using PagedList;

namespace Bshkara.Web.ViewModels.Bases
{
    public abstract class BaseListViewModel<T> : BaseViewModel
    {
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }

            set
            {
                _pageSize = value;
                PagingHelper.SaveCurrentPageSizeToSession(value);
                SetCurrentValueForDropDownPageSize(_pageSize);
            }
        }

        public string SearchString { get; set; }
        public SelectList ItemsPerPageList { get; set; }
        public StaticPagedList<T> Items { get; set; }

        private void SetCurrentValueForDropDownPageSize(int value)
        {
            ItemsPerPageList = new SelectList(PagingHelper.PageSizes, value);
        }
    }
}