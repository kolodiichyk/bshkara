using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Nationalities
    /// </summary>
    [Table("Nationalities")]
    public class NationalityEntity : AuditedDictionary
    {
    }
}