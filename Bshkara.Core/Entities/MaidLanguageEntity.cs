using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bashkra.Shared.Enums;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// What languages maid know
    /// </summary>
    [Table("MaidLanguages")]
    public class MaidLanguageEntity : AuditedEntity
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
        /// <c>Language</c> code
        /// </summary>
        public Guid LanguageId { get; set; }

        /// <summary>
        /// <c>Language object</c>
        /// </summary>
        [ForeignKey("LanguageId")]
        public virtual LanguageEntity Language { get; set; }

        /// <summary>
        /// How good maid speak on this language
        /// </summary>
        public Level SpokenLevel { get; set; }

        /// <summary>
        /// How good maid read in this language
        /// </summary>
        public Level ReadLevel { get; set; }

        /// <summary>
        /// How good maid write in this language
        /// </summary>
        public Level WrittenLevel { get; set; }
    }
}