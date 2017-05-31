using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Bshkara.Core.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// User role
    /// </summary>
    [Table("UserRoles")]
    public class UserRoleEntity : IdentityUserRole<Guid>
    {
    }

    /// <summary>
    /// User claim
    /// </summary>
    [Table("UserClaims")]
    public class UserClaimEntity : IdentityUserClaim<Guid>
    {
    }

    /// <summary>
    /// User login
    /// </summary>
    [Table("UserLogins")]
    public class UserLoginEntity : IdentityUserLogin<Guid>
    {
    }

    /// <summary>
    /// User role
    /// </summary>
    [Table("Roles")]
    public class RoleEntity : IdentityRole<Guid, UserRoleEntity>
    {
        /// <summary>
        /// Admin role
        /// </summary>
        public const string AdminRoleName = "Admin";

        /// <summary>
        /// Agent admin role
        /// </summary>
        public const string AgentAdminRoleName = "AgentAdmin";

        /// <summary>
        /// Agent admin role
        /// </summary>
        public const string AgentUserRoleName = "AgentUser";

        /// <summary>
        /// Role
        /// </summary>
        public RoleEntity()
        {
        }

        /// <summary>
        /// Role
        /// </summary>
        public RoleEntity(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// User
    /// </summary>
    [Table("Users")]
    public class UserEntity : IdentityUser<Guid, UserLoginEntity, UserRoleEntity, UserClaimEntity>, IAuditedEntity
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User mobile number
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Who create code
        /// </summary>
        public Guid? CreatedById { get; set; }

        /// <summary>
        /// Who update code
        /// </summary>
        public Guid? UpdatedById { get; set; }

        /// <summary>
        /// Date of create
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Date of update
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Who create
        /// </summary>
        [ForeignKey("CreatedById")]
        public UserEntity CreatedBy { get; set; }

        /// <summary>
        /// Who update
        /// </summary>
        [ForeignKey("UpdatedById")]
        public UserEntity UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserEntity, Guid> manager,
            string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, Name));
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, Email));

            // Add custom user claims here
            return userIdentity;
        }
    }
}