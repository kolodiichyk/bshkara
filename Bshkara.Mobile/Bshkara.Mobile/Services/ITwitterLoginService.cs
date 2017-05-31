using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bshkara.Mobile.Services
{
    public interface ITwitterLoginService
    {
        Task<FacebookLoginResult> Login();
    }
}