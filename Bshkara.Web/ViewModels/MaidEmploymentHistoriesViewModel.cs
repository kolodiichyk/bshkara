using System;
using Bshkara.Core.Entities;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.ViewModels
{
    public class MaidEmploymentHistoriesViewModel : BaseListViewModel<MaidEmploymentHistoryEntity>
    {
        public Guid? MaidId { get; set; }
    }
}