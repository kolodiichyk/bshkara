using System;
using Bshkara.Core.Entities;

namespace Bshkara.Core.Base
{
    public interface IAuditedEntity : IIdentityEntity
    {
        /// <summary>
        /// Date of create
        /// </summary>
        DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Date of update
        /// </summary>
        DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Who create code
        /// </summary>
        Guid? CreatedById { get; set; }

        /// <summary>
        /// Who update code
        /// </summary>
        Guid? UpdatedById { get; set; }

        /// <summary>
        /// Who create
        /// </summary>
        UserEntity CreatedBy { get; set; }

        /// <summary>
        /// Who update
        /// </summary>
        UserEntity UpdatedBy { get; set; }
    }
}