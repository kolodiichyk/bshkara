using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Bshkara.Mobile.Services;
using Newtonsoft.Json.Linq;
using UIKit;
using Xamarin.Auth;


namespace Bshkara.Mobile.iOS.Services
{
    public class TwitterLoginService : ITwitterLoginService
    {
        public Task<FacebookLoginResult> Login()
        {

            var result = new TaskCompletionSource<FacebookLoginResult>();
            var auth = new OAuth1Authenticator(
                    "wDDuNu2auh21NLhWQu2zOdhSc",
                    "QYeJH178DVJB6hSovnbWoTueHKo87WGNAPNLMmCkfcNctlaHEw",
                    new Uri("https://api.twitter.com/oauth/request_token"),
                    new Uri("https://api.twitter.com/oauth/authorize"),
                    new Uri("https://api.twitter.com/oauth/access_token"),
                    new Uri("http://mobile.twitter.com/home")
                )
                {AllowCancel = true};

            auth.Completed += async (s, eventArgs) =>
            {
                UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);

                var twitterLoginResult = new FacebookLoginResult();
                if (eventArgs.IsAuthenticated)
                {
                    var loggedInAccount = eventArgs.Account;
                    twitterLoginResult = new FacebookLoginResult
                    {
                        Name = loggedInAccount.Properties["screen_name"],
                        AccessToken = loggedInAccount.Properties["oauth_token"],
                        UserId = loggedInAccount.Properties["user_id"]
                    };

                    twitterLoginResult.Email = await GetTwitterUserMail(loggedInAccount);
                }

                twitterLoginResult.IsLoggedIn = eventArgs.IsAuthenticated;
                result.SetResult(twitterLoginResult);
            };

            UIApplication.SharedApplication.KeyWindow.RootViewController
                .PresentViewController(auth.GetUI(), true, null);

            return result.Task;
        }

        private async Task<string> GetTwitterUserMail(Account account)
        {
            var request = new OAuth1Request(
                "GET",
                new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                new Dictionary<string, string>
                {
                    {"include_email", "true"},
                    {"skip_status", "true"},
                    {"include_entities", "true"}
                },
                account);

            var res = await request.GetResponseAsync();
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var resString = res.GetResponseText();
                var o = JObject.Parse(resString);
                return (string)o["email"];
            }

            return null;
        }
    }
}