using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Helpers.Extenstions
{
    public static class NavigationExtensions
    {
        public static NavigateHelper<TViewModel> For<TViewModel>(this ViewModelNavigation viewModelNavigation)
            where TViewModel : ViewModel
        {
            return new NavigateHelper<TViewModel>().AttachTo(viewModelNavigation);
        }
    }
}