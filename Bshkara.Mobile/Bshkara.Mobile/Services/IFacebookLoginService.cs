using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bshkara.Mobile.Services
{
    public interface IFacebookLoginService
    {
        Task<FacebookLoginResult> Login();

        Task<Dictionary<string, string>> QueryFacebookApi(string query);
    }
}