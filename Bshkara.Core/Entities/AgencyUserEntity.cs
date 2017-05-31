using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    [Table("AgencyUsers")]
    public class AgencyUserEntity : AuditedEntity
    {
        /// <summary>
        /// <c>Agency</c> code
        /// </summary>
        public Guid AgencyId { get; set; }

        /// <summary>
        /// <c>Agency</c>
        /// </summary>
        [ForeignKey("AgencyId")]
        public virtual AgencyEntity Agency { get; set; }

        /// <summary>
        /// <c>User</c> code
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// <c>User</c>
        /// </summary>
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}