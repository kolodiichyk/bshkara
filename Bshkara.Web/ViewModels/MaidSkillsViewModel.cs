using System;
using Bshkara.Core.Entities;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.ViewModels
{
    public class MaidSkillsViewModel : BaseListViewModel<MaidSkillEntity>
    {
        public Guid? MaidId { get; set; }
    }
}