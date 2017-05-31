using System.Windows.Input;
using Bashkra.ApiClient.Requests;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.Services.WebService;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IWebService _webService;

        public SignUpViewModel(IWebService webService)
        {
            _webService = webService;
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public ICommand SignUpCommand => new Command(SignUpAsync);

        private async void SignUpAsync()
        {
            App.LoginAsync(await _webService.SignUpAsync(new SignUpArgs
            {
                UserName = UserName,
                Email = Email,
                Phone = Phone,
                Password = Password,
                ConfirmedPassword = ConfirmedPassword
            }), async () =>
            {
                await Navigation.For<HomeViewModel>().Navigate();
                App.EnebleMenu();
                await Navigation.RemoveAsync(this);
            });
        }
    }
}