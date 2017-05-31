using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;
using Bshkara.Core.Localization;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Document type
    /// </summary>
    [Table("DocumentType")]
    public class DocumentTypeEntity : AuditedDictionary
    {
        [Display(Name = "DocumentTypeEntity_ShowAsPicture", ResourceType = typeof(Res))]
        public bool ShowAsPicture { get; set; }

        public string Icon { get; set; }
    }
}