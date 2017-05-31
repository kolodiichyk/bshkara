using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bshkara.Core.Base
{
    /// <summary>
    /// Abstract identity
    /// </summary>
    public abstract class IdentityEntity : IIdentityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}