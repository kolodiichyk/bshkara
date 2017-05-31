using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bshkara.Mobile.iOS.Helpers;
using Bshkara.Mobile.Services;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using Xamarin.Forms.Platform.iOS;

namespace Bshkara.Mobile.iOS.Services
{
    public class FacebookLoginService : IFacebookLoginService
    {
        private readonly string[] _defaultPermissions = { "public_profile", "email", "user_location" };

        public async Task<Dictionary<string, string>> QueryFacebookApi(string query)
        {
                var result = new Dictionary<string, string>();

                var request = new GraphRequest(query, null);

                var graphTask = new TaskCompletionSource<NSObject>();

                try
                {
                    request.Start((connection, graphApiResult, error) => { graphTask.TrySetResult(graphApiResult); });
                }
                catch (Exception e)
                {
                    graphTask.SetException(e);
                }

                var nsObject = (NSDictionary)await graphTask.Task;
                foreach (var key in nsObject.Keys)
                {
                    result[key.ToString()] = nsObject[key].ToString();
                }

                return result;
        }

        public async Task<FacebookLoginResult> Login()
        {
            try
            {
                var manager = new LoginManager { LoginBehavior = LoginBehavior.Native };
                var loginResult = await manager.LogInWithReadPermissionsAsync(_defaultPermissions, await iOSHelper.GetCurrentViewControllerAsync());
                if (loginResult.IsCancelled)
                {
                    return new FacebookLoginResult { IsLoggedIn = false};
                }

                var expirationTime = loginResult.Token.ExpirationDate.ToDateTime();
                var token = loginResult.Token.TokenString;
                var userId = loginResult.Token.UserID;

                Dictionary<string, string> userResult = await QueryFacebookApi("/me?fields=email,name,location");

                string locationString = userResult["location"];

                return new FacebookLoginResult
                {
                    AccessToken = token,
                    ExpirationTime = expirationTime,
                    UserId = userId,
                    IsLoggedIn = true,
                    Email = userResult.ContainsKey("email") ? userResult["email"] : null,
                    Name = userResult.ContainsKey("name") ? userResult["name"] : null,
                    Location = (string)locationString
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new FacebookLoginResult
                {
                    IsLoggedIn = false
                };
            }
        }
    }
}