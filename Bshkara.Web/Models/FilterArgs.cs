using System;
using Bashkra.Shared.Enums;
using Bshkara.Web.Helpers;

namespace Bshkara.Web.Models
{
    public class FilterArgs
    {
        public FilterArgs()
        {
            PageNumber = PagingHelper.DefaultPageNumber;
            PageSize = PagingHelper.GetCurrentPageSizeFromSession();
        }

        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid MaidId { get; set; }

        public BookingStatus? BookingStatus { get; set; }

        public Guid UserId { get; set; }

        public Guid AgencyId { get; set; }

        public DateTime? FilterDateStart { get; set; } //DevHistory
        public DateTime? FilterDateEnd { get; set; } //DevHistory
    }
}