using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    [Table("ApiTokens")]
    public class ApiTokenEntity : AuditedEntity
    {
        public ApiTokenEntity()
        {
            Token = GenerateToken();
        }

        public string Name { get; set; }

        public string Token { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }

        public bool IsBloked { get; set; }

        public bool IsDeleted { get; set; }

        public string WhiteDomainList { get; set; }

        public static string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}