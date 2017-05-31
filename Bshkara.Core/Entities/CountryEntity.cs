using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    ///     Country />
    /// </summary>
    [Table("Countries")]
    public class CountryEntity : AuditedDictionary
    {
        public string CountryCode { get; set; }

        public virtual List<CityEntity> Cities { get; set; }
    }
}