using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    ///     Cities />
    /// </summary>
    [Table("Cities")]
    public class CityEntity : AuditedDictionary
    {
        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual CountryEntity Country { get; set; }
    }
}