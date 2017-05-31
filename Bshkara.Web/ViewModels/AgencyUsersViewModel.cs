using System;
using Bshkara.Core.Entities;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.ViewModels
{
    public class AgencyUsersViewModel : BaseListViewModel<AgencyUserEntity>
    {
        public Guid? AgencyId { get; set; }
    }
}