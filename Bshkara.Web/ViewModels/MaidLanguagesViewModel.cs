using System;
using Bshkara.Core.Entities;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.ViewModels
{
    public class MaidLanguagesViewModel : BaseListViewModel<MaidLanguageEntity>
    {
        public Guid? MaidId { get; set; }
    }
}