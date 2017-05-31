using System;
using Acr.UserDialogs;
using Autofac;
using Bashkra.ApiClient.Responses;
using Bashkra.Shared.Enums;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Localization;
using Bshkara.Mobile.Localization.Resources;
using Bshkara.Mobile.Services.WebService;
using Bshkara.Mobile.ViewModels;
using Bshkara.Mobile.Views;
using Bshkara.Mobile.Views.AgenciesView;
using Bshkara.Mobile.Views.HomeView;
using Bshkara.Mobile.Views.ImageViewerView;
using Bshkara.Mobile.Views.WelcomeSlidsView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Ioc.Autofac;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Bshkara.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Initialize();
            InitializeLocalization();
        }

        public static IUserDialogs Dialogs => Resolver.Resolve<IUserDialogs>();

        private void Initialize()
        {
            RegisterServices();
            RegisterViews();

            if (Settings.User == null)
                SetMainPage((Page) ViewFactory.CreatePage<LoginViewModel, LoginView>(), false);
            else if (!Settings.IsFirstRun)
                SetMainPage((Page) ViewFactory.CreatePage<HomeViewModel, HomeView>());
            else
                SetMainPage((Page) ViewFactory.CreatePage<WelcomeSlidsViewModel, WelcomeSlidsView>(), false);
        }

        /// <summary>
        ///     Set main page
        /// </summary>
        /// <param name="mainPage"></param>
        public static void SetMainPage(Page mainPage, bool menuVisibility = true)
        {
            var masterDetailPage = Current.MainPage as MasterDetailPage;
            if (masterDetailPage == null)
            {
                var masterPage = (Page) ViewFactory.CreatePage<MenuViewModel, MenuView>();
                masterPage.Title = "Menu";
                mainPage.Title = "Bshkara";

                masterDetailPage = new MasterDetailPage
                {
                    Master = masterPage,
                    Detail = new NavigationPage(mainPage)
                };
            }
            else
            {
                masterDetailPage.Detail = new NavigationPage(mainPage);
            }

            Current.MainPage = masterDetailPage;

            if (menuVisibility)
                EnebleMenu();
            else
                DisableMenu();
        }

        public static void EnebleMenu()
        {
            var masterDetailPage = Current.MainPage as MasterDetailPage;
            if (masterDetailPage == null)
                return;

            masterDetailPage.Master.IsVisible = true;
            masterDetailPage.IsGestureEnabled = true;
        }

        public static void DisableMenu()
        {
            var masterDetailPage = Current.MainPage as MasterDetailPage;
            if (masterDetailPage == null)
                return;

            masterDetailPage.Master.IsVisible = false;
            masterDetailPage.IsGestureEnabled = false;
        }

        public static event Action<IDependencyContainer> OnContainerSet;

        private void RegisterViews()
        {
            ViewFactory.Register<LoginView, LoginViewModel>();
            ViewFactory.Register<SignUpView, SignUpViewModel>();
            ViewFactory.Register<MenuView, MenuViewModel>();
            ViewFactory.Register<HomeView, HomeViewModel>();
            ViewFactory.Register<MaidDetailsView, MaidDetailsViewModel>();
            ViewFactory.Register<FilterView, FilterViewModel>();
            ViewFactory.Register<SelectableView, NationalitiesViewModel>();
            ViewFactory.Register<SelectableView, CitiesViewModel>();
            ViewFactory.Register<WelcomeSlidsView, WelcomeSlidsViewModel>();
            ViewFactory.Register<AgenciesView, AgenciesViewModel>();
            ViewFactory.Register<ImageViewerView, ImageViewerViewModel>();
        }

        private static void RegisterServices()
        {
            var builder = new ContainerBuilder();
            var container = new AutofacContainer(builder.Build());

            //Allow native to register
            OnContainerSet?.Invoke(container);

            //register PCL Services
            container.Register<IWebService, WebService>();
            container.Register<IDependencyContainer>(container);

            //Set global resolver
            if (!Resolver.IsSet)
                Resolver.SetResolver(container.GetResolver());
        }

        private void InitializeLocalization()
        {
            var localize = Resolver.Resolve<ILocalize>();
            if (Device.OS != TargetPlatform.WinPhone)
                AppResources.Culture = localize.GetCurrentCultureInfo();
        }

        public static void LoginAsync(SignInApiResponse logIn, Action onSuccess, Action<string> onUnsuccess = null)
        {
            if (!logIn.Success)
            {
                onUnsuccess?.Invoke(logIn.Error);
                Dialogs.ErrorToast("error", logIn.Error);
                return;
            }

            switch (logIn.SignInStatus)
            {
                case SignInStatus.Success:

                    Settings.User = logIn.User;

                    var webService = Resolver.Resolve<IWebService>();
                    webService.ChangeAccessToken(logIn.AccessToken);

                    onSuccess.Invoke();
                    break;
                case SignInStatus.Failure:
                    Dialogs.ErrorToast("error", AppResources.Login_Failure);
                    break;
                case SignInStatus.InvalidLoginOrPassword:
                    Dialogs.ErrorToast("error", AppResources.Login_InvalidLoginOrPassword);
                    break;
                case SignInStatus.LockedOut:
                    Dialogs.ErrorToast("error", AppResources.Login_LockedOut);
                    break;
            }
        }
    }
}