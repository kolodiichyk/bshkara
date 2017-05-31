using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Web.Services;
using Microsoft.AspNet.Identity;

namespace Bshkara.Web.Extentions
{
    public static class IIdentityExtensions
    {
        public static Guid GetUserGuidId(this IIdentity identity)
        {
            Guid result;
            var id = identity.GetUserId();
            Guid.TryParse(id, out result);
            return result;
        }

        public static string GetUserName(this IIdentity identity)
        {
            return IdentityExtensions.GetUserName(identity);
        }

        public static AgencyEntity GetAgency(this IIdentity identity)
        {
            var id = identity.GetUserGuidId();

            var agencyUsersService = DependencyResolver.Current.GetService<AgencyUsersService>();
            var agencyUser =
                agencyUsersService.UnitOfWork.Context.Set<AgencyUserEntity>().FirstOrDefault(t => t.UserId == id);

            return agencyUser?.Agency;
        }

        public static Guid? GetAgencyId(this IIdentity identity)
        {
            return identity.GetAgency()?.Id;
        }


        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return IdentityExtensions.FindFirstValue(identity, claimType);
        }
    }
}