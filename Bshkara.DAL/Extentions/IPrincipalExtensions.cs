using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Bshkara.DAL.Extentions
{
    public static class IPrincipalExtensions
    {
        public static Guid? GetId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity) user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return claim == null ? (Guid?) null : Guid.Parse(claim.Value);
        }

        public static string GetDisplayName(this IPrincipal user)
        {
            var firstName = GetFirstName(user);
            var lastName = GetLastName(user);
            var email = GetEmail(user);

            return firstName != "" ? $"{firstName} {lastName}" : email;
        }

        public static string GetRolesAsString(this IPrincipal user)
        {
            var claims = ((ClaimsIdentity) user.Identity).FindAll(ClaimTypes.Role).Select(x => x.Value);
            return string.Join(", ", claims);
        }

        public static string GetFirstName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity) user.Identity).FindFirst(ClaimTypes.GivenName);
            return claim == null ? null : claim.Value;
        }

        public static string GetLastName(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity) user.Identity).FindFirst(ClaimTypes.Surname);
            return claim == null ? null : claim.Value;
        }

        public static string GetEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity) user.Identity).FindFirst(ClaimTypes.Email);
            return claim == null ? null : claim.Value;
        }
    }
}