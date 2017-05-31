using System;
using Bshkara.Core.Entities;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.ViewModels
{
    public class AgencyPacksViewModel : BaseListViewModel<AgencyPackageEntity>
    {
        public Guid? AgencyId { get; set; }
    }
}