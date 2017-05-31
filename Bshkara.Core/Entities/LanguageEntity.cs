using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Language that maid can know
    /// </summary>
    [Table("Languages")]
    public class LanguageEntity : AuditedDictionary
    {
        public string ShortName { get; set; }
    }
}