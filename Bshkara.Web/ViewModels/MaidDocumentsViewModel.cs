using System;
using Bshkara.Core.Entities;
using Bshkara.Web.ViewModels.Bases;

namespace Bshkara.Web.ViewModels
{
    public class MaidDocumentsViewModel : BaseListViewModel<MaidDocumentEntity>
    {
        public Guid? MaidId { get; set; }
    }
}