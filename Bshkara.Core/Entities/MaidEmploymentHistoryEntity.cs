using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;
using Bshkara.Core.Localization;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Maid previous employment history
    /// </summary>
    [Table("MaidEmploymentHistory")]
    public class MaidEmploymentHistoryEntity : AuditedEntity
    {
        /// <summary>
        /// <c>Maid</c> code
        /// </summary>
        [Required]
        public Guid MaidId { get; set; }

        /// <summary>
        /// <c>Maid object</c>
        /// </summary>
        [ForeignKey("MaidId")]
        public virtual MaidEntity Maid { get; set; }

        /// <summary>
        /// <c>Country</c> code
        /// </summary>
        [Required]
        public Guid? CountryId { get; set; }

        /// <summary>
        /// <c>Country object</c>
        /// </summary>
        [ForeignKey("CountryId")]
        public virtual CountryEntity Country { get; set; }

        /// <summary>
        /// Employment duration
        /// </summary>
        [Required]
        [Display(Name = "MaidEmploymentHistoryEntity_Duration", ResourceType = typeof(Res))]
        public decimal Duration { get; set; }

        /// <summary>
        /// Employment description
        /// </summary>
        [Required]
        public LocalizedName Descripion { get; set; }
    }
}