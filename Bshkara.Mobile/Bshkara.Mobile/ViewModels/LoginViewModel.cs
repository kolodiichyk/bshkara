using System.Windows.Input;
using Bashkra.ApiClient.Requests;
using Bashkra.Shared.Enums;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.Services;
using Bshkara.Mobile.Services.WebService;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Bshkara.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IWebService _webService;

        public LoginViewModel(IWebService webService)
        {
            State = WaitPageState.Content;
            _webService = webService;
        }

        public string Email { get; set; } = "admin@bshkara.com";

        public string Password { get; set; } = "bshkara";

        public ICommand LoginCommand => new Command(LoginAsync);

        public ICommand FacebookLoginCommand => new Command(FacebookLoginAsync);

        public ICommand TwitterLoginCommand => new Command(TwitterLoginAsync);

        public ICommand SignUpCommand => new Command(SignUpAsync);

        public WaitPageState State { get; set; }

        private async void TwitterLoginAsync()
        {
            var twitterLoginService = Resolver.Resolve<ITwitterLoginService>();
            var result = await twitterLoginService.Login();
            if (result.IsLoggedIn)
            {
                State = WaitPageState.Wait;
                App.LoginAsync(await _webService.LogInAsync(new ExternalSignInArgs
                {
                    Email = result.Email,
                    Name = result.Name,
                    LoginProvider = LoginProvider.Twitter,
                    ProviderKey = result.UserId,
                    Token = result.AccessToken
                }), AfterSuccessLogin, s => State = WaitPageState.Content);
            }
        }

        private async void FacebookLoginAsync()
        {
            var facebookLoginService = Resolver.Resolve<IFacebookLoginService>();
            var result = await facebookLoginService.Login();
            if (result.IsLoggedIn)
            {
                State = WaitPageState.Wait;
                App.LoginAsync(await _webService.LogInAsync(new ExternalSignInArgs
                {
                    Email = result.Email,
                    Name = result.Name,
                    LoginProvider = LoginProvider.Facebook,
                    ProviderKey = result.UserId,
                    Token = result.AccessToken
                }), AfterSuccessLogin, s => State = WaitPageState.Content);
            }
        }

        private async void LoginAsync()
        {
            State = WaitPageState.Wait;
            App.LoginAsync(await _webService.LogInAsync(Email, Password), AfterSuccessLogin,
                s => State = WaitPageState.Content);
        }

        public async void AfterSuccessLogin()
        {
            if (Settings.IsFirstRun)
            {
                await Navigation.For<WelcomeSlidsViewModel>().Navigate();
                App.DisableMenu();
            }
            else
            {
                await Navigation.For<HomeViewModel>().Navigate();
                App.EnebleMenu();
            }
            await Navigation.RemoveAsync(this);
        }

        private async void SignUpAsync()
        {
            await Navigation.For<SignUpViewModel>().Navigate();
        }
    }
}