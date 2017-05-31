using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Visa status
    /// </summary>
    [Table("VisaStatus")]
    public class VisaStatusEntity : AuditedDictionary
    {
    }
}