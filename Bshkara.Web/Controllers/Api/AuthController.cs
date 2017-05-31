using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;
using Bshkara.Core.Entities;
using Bshkara.DAL.Identity;
using Bshkara.Web.Helpers.Extentions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Bshkara.Web.Controllers.Api
{
    [Authorize]
    public class AuthController : BaseApiController
    {
        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ApplicationSignInManager SignInManager
            => Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();

        /// <summary>
        ///     Sign in to get token
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [ActionName("signin")]
        public async Task<SignInApiResponse> SignInAsync(SignInArgs args)
        {
            return await TryInvoceAsync(async () =>
            {
                var response = new SignInApiResponse {Lang = Language};

                if (args == null)
                {
                    response.Error = "json parameters are empty";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(args.Email))
                {
                    response.Error = BshkaraRes.Api_Auth_EmailIsEmpty;
                    return response;
                }

                if (!args.Email.IsValidEmail())
                {
                    response.Error = BshkaraRes.Api_Auth_EmailIsNotValid;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(args.Password))
                {
                    response.Error = BshkaraRes.Api_Auth_PasswordIsEmpty;
                    return response;
                }

                // Require the user to have a confirmed email before they can log on.
                var user = await UserManager.FindByNameAsync(args.Email);
                if (user == null)
                {
                    response.Error = BshkaraRes.Api_Auth_UserNotExists;
                    return response;
                }

                if (user != null)
                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        response.Error = BshkaraRes.Api_Auth_EmailMustBeConfirmed;
                        return response;
                    }

                var result = await SignInManager.PasswordSignInAsync(args.Email, args.Password, true, true);
                switch (result)
                {
                    case SignInStatus.Success:

                        response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.Success;

                        var climseIdenty = await user.GenerateUserIdentityAsync(UserManager);
                        var ticket = new AuthenticationTicket(climseIdenty, new AuthenticationProperties());
                        var currentUtc = new SystemClock().UtcNow;
                        ticket.Properties.IssuedUtc = currentUtc;
                        ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddDays(365));
                        ticket.Properties.IsPersistent = false;

                        response.AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

                        response.User = new ApiUser
                        {
                            Name = user.Name,
                            Mobile = user.Mobile,
                            Email = user.Email,
                            Id = user.Id
                        };

                        break;
                    case SignInStatus.LockedOut:
                        response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.LockedOut;
                        response.Error = BshkaraRes.Api_Auth_Lockout;
                        break;
                    case SignInStatus.RequiresVerification:
                        response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.RequiresVerification;
                        response.Error = "RequiresVerification";
                        break;
                    case SignInStatus.Failure:
                    default:
                        response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.InvalidLoginOrPassword;
                        response.Error = BshkaraRes.Api_Auth_InvalidLogin;
                        break;
                }

                return response;
            });
        }

        [HttpPost]
        [ActionName("signout")]
        public ApiResponse SignOut()
        {
            return TryInvoce(() =>
            {
                var response = new ApiResponse {Lang = Language};
                Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return response;
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("signup")]
        public async Task<SignInApiResponse> SignUp(SignUpArgs args)
        {
            var response = new SignInApiResponse {Lang = Language};

            if (args == null)
            {
                response.Error = "json parameters are empty";
                return response;
            }

            if (string.IsNullOrWhiteSpace(args.Email))
            {
                response.Error = BshkaraRes.Api_Auth_EmailIsEmpty;
                return response;
            }

            if (!args.Email.IsValidEmail())
            {
                response.Error = BshkaraRes.Api_Auth_EmailIsNotValid;
                return response;
            }

            if (string.IsNullOrWhiteSpace(args.Password))
            {
                response.Error = BshkaraRes.Api_Auth_PasswordIsEmpty;
                return response;
            }

            if (string.IsNullOrWhiteSpace(args.ConfirmedPassword))
            {
                response.Error = BshkaraRes.Api_Auth_ConfirmatedPasswordEmpty;
                return response;
            }

            if (args.Password != args.ConfirmedPassword)
            {
                response.Error = BshkaraRes.Api_Auth_PasswordNotEqualsConfirmatedPassword;
                return response;
            }

            var user = new UserEntity {UserName = args.UserName, Email = args.Email};
            var result = await UserManager.CreateAsync(user, args.Password);
            if (result.Succeeded)
                return await SignInAsync(new SignInArgs {Email = user.Email, Password = args.Password});

            response.Error = BshkaraRes.Api_Auth_CantCreateUser;

            return response;
        }

        /// <summary>
        ///     Sign by external provider in to get token
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [ActionName("external_signin")]
        public async Task<SignInApiResponse> ExternalSignInAsync(ExternalSignInArgs args)
        {
            return await TryInvoceAsync(async () =>
            {
                var response = new SignInApiResponse {Lang = Language};

                var user =
                    await UserManager.FindAsync(new UserLoginInfo(args.LoginProvider.ToString(), args.ProviderKey));

                var hasRegistered = user != null;

                if (!hasRegistered)
                {
                    user = new UserEntity
                    {
                        Id = Guid.NewGuid(),
                        UserName = args.Email,
                        Name = args.Name,
                        Email = args.Email,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var password = Guid.NewGuid().ToString();
                    var result = await UserManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.Failure;
                        response.Error = BshkaraRes.Api_Auth_InvalidLogin;
                        return response;
                    }

                    var info = new ExternalLoginInfo
                    {
                        DefaultUserName = args.Email,
                        Login = new UserLoginInfo(args.LoginProvider.ToString(), args.ProviderKey)
                    };

                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (!result.Succeeded)
                    {
                        response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.Failure;
                        response.Error = BshkaraRes.Api_Auth_InvalidLogin;
                        return response;
                    }

                    var identity = await UserManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
                    Authentication.SignIn(identity);
                }

                var climseIdenty = await user.GenerateUserIdentityAsync(UserManager);
                var ticket = new AuthenticationTicket(climseIdenty, new AuthenticationProperties());
                var currentUtc = new SystemClock().UtcNow;
                ticket.Properties.IssuedUtc = currentUtc;
                ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddDays(365));
                ticket.Properties.IsPersistent = false;

                response.AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
                response.SignInStatus = Bashkra.Shared.Enums.SignInStatus.Success;
                response.User = new ApiUser
                {
                    Name = user.Name,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    Id = user.Id
                };

                return response;
            });
        }
    }
}