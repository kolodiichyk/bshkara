using System.ComponentModel.DataAnnotations;
using Bshkara.Core.Localization;

namespace Bshkara.Core.Base
{
    /// <summary>
    /// Audited dictionary
    /// </summary>
    public abstract class AuditedDictionary : AuditedEntity
    {
        /// <summary>
        /// Entity name
        /// </summary>
        [Display(Name = "AuditedDictionary_Name", ResourceType = typeof (Res))]
        public LocalizedName Name { get; set; }
    }
}