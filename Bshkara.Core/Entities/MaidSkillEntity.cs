using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bashkra.Shared.Enums;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// What skills maid has
    /// </summary>
    [Table("MaidSkills")]
    public class MaidSkillEntity : AuditedEntity
    {
        /// <summary>
        /// <c>Maid</c> code
        /// </summary>
        public Guid MaidId { get; set; }

        /// <summary>
        /// <c>Maid object</c>
        /// </summary>
        [ForeignKey("MaidId")]
        public virtual MaidEntity Maid { get; set; }

        /// <summary>
        /// <c>Skil</c> code
        /// </summary>
        public Guid SkillId { get; set; }

        /// <summary>
        /// <c>Skill object</c>
        /// </summary>
        [ForeignKey("SkillId")]
        public virtual SkillEntity Skill { get; set; }


        /// <summary>
        /// How good maid know skill
        /// </summary>
        public Level SkillLevel { get; set; }
    }
}