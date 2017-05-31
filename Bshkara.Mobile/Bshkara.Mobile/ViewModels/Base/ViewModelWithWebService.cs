using Bshkara.Mobile.Services.WebService;

namespace Bshkara.Mobile.ViewModels
{
    public class ViewModelWithWebService : BaseHomeViewModel
    {
        protected readonly IWebService WebService;

        public ViewModelWithWebService(IWebService webService)
        {
            WebService = webService;
        }
    }
}