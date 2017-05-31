using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Entities;

namespace Bshkara.Core.Base
{
    /// <summary>
    /// Entity with audited
    /// </summary>
    public abstract class AuditedEntity : IdentityEntity, IAuditedEntity
    {
        /// <summary>
        /// Date of create
        /// </summary>
        [DataType(DataType.DateTime),
         DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Date of update
        /// </summary>
        [DataType(DataType.DateTime),
         DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Who create code
        /// </summary>
        public Guid? CreatedById { get; set; }

        /// <summary>
        /// Who update code
        /// </summary>
        public Guid? UpdatedById { get; set; }

        /// <summary>
        /// Who create
        /// </summary>
        [ForeignKey("CreatedById")]
        public virtual UserEntity CreatedBy { get; set; }

        /// <summary>
        /// Who update
        /// </summary>
        [ForeignKey("UpdatedById")]
        public virtual UserEntity UpdatedBy { get; set; }
    }
}