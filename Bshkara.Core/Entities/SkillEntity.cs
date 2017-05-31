using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Skills that maid can have
    /// </summary>
    [Table("Skills")]
    public class SkillEntity : AuditedDictionary
    {
        public string Icon { get; set; }
    }
}