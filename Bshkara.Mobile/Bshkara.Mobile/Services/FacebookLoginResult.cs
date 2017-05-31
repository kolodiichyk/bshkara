using System;

namespace Bshkara.Mobile.Services
{
    public class FacebookLoginResult
    {
        public DateTime ExpirationTime { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}