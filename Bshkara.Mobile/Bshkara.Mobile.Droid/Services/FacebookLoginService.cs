using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Bshkara.Mobile.Services;
using Java.Util;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using Debug = System.Diagnostics.Debug;

//using Xamarin.Facebook.Login;

namespace Bshkara.Mobile.Droid.Services
{
    public class FacebookLoginService : IFacebookLoginService
    {
        private readonly string[] _defaultPermissions = {"public_profile", "email", "user_location"};

        public async Task<FacebookLoginResult> Login()
        {
            var activity = Forms.Context as MainActivity;
            if (activity == null)
                throw new Exception("MainActivity is not context.");

            var waiter = new TaskCompletionSource<FacebookLoginResult>();
            Action setFacebookCancellation = () => waiter.SetCanceled();
            Action<Exception> setFacebookLoginError = exception => waiter.SetException(exception);
            Action<AccessToken> setFacebookLoginResult = async token =>
            {
                var localQuery = await LocalQuery(token, "me");

                waiter.SetResult(new FacebookLoginResult
                {
                    AccessToken = token.Token,
                    ExpirationTime = ToDateTime(token.Expires),
                    UserId = token.UserId,
                    IsLoggedIn = true,
                    Name = localQuery?["name"],
                    Email = localQuery?["email"],
                    Location = localQuery?["location"]
                });
            };

            try
            {
                var manager = LoginManager.Instance;
                manager.SetLoginBehavior(LoginBehavior.NativeWithFallback);
                manager.LogInWithReadPermissions(activity, _defaultPermissions);


                activity.SetFacebookLoginResult += setFacebookLoginResult;
                activity.SetFacebookLoginError += setFacebookLoginError;
                activity.SetFacebookCancellation += setFacebookCancellation;


                return await waiter.Task;
            }
            catch (Exception  e)
            {
                Debug.WriteLine(e);
                return new FacebookLoginResult
                {
                    IsLoggedIn = false
                };
            }
            finally
            {
                activity.SetFacebookLoginResult -= setFacebookLoginResult;
                activity.SetFacebookLoginError -= setFacebookLoginError;
                activity.SetFacebookCancellation -= setFacebookCancellation;
            }
        }

        public Task<Dictionary<string, string>> QueryFacebookApi(string query)
        {
            return Task.FromResult<Dictionary<string, string>>(null);
        }

        private async Task<Dictionary<string, string>> LocalQuery(AccessToken token, string query)
        {
            var parameters = new Bundle();
            parameters.PutString("fields", "id,name,email,location");

            var request = new GraphRequest(token, query, parameters, HttpMethod.Get);

            try
            {
                var graphREsponse = await Task.Run(() => request.ExecuteAndWait());
                if (graphREsponse == null) return null;

                var email = graphREsponse.JSONObject.GetString("email");
                var name = graphREsponse.JSONObject.GetString("name");
                var location = graphREsponse.JSONObject.GetJSONObject("location").GetString("name");
                return new Dictionary<string, string>
                {
                    {"email", email},
                    {"name", name},
                    {"location", location}
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return null;
        }

        private
            DateTime ToDateTime(Date
                date)
        {
            var javaDateAsMilliseconds = date.Time;
            var dateTime =
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Add(
                    TimeSpan.FromMilliseconds(javaDateAsMilliseconds));
            return dateTime;
        }
    }
}