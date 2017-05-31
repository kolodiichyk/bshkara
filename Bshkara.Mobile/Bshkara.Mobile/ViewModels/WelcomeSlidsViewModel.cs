using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class WelcomeSlidsViewModel : BaseViewModel
    {
        public WelcomeSlidsViewModel()
        {
            Views = new ObservableCollection<CarouselViewModel>(new[] {FirstView, SecondView, ThirdView, FourthView});
        }

        public CarouselViewModel FirstView { get; set; } = new CarouselViewModel();

        public CarouselViewModel SecondView { get; set; } = new CarouselViewModel();

        public CarouselViewModel ThirdView { get; set; } = new CarouselViewModel();

        public CarouselViewModel FourthView { get; set; } = new CarouselViewModel();

        public ObservableCollection<CarouselViewModel> Views { get; set; }

        public bool GotItVisibility { get; set; }

        public bool NextVisibility { get; set; } = true;

        public bool SkipVisibility { get; set; } = true;

        public CarouselViewModel SelectedItem { get; set; }

        public ICommand NextCommand => new Command(Next);

        public ICommand SkipCommand => new Command(Skip);

        public override void OnViewAppearing()
        {
            SelectedItem = Views.FirstOrDefault();
        }

        private async void Skip()
        {
            await Navigation.For<HomeViewModel>().Navigate();
            App.EnebleMenu();
            Settings.IsFirstRun = false;
            await Navigation.RemoveAsync(this);
        }

        private void Next()
        {
            var currentIndex = Views.IndexOf(SelectedItem);
            if (currentIndex + 1 <= Views.Count)
                SelectedItem = Views[currentIndex + 1];
        }

        protected override void NotifyPropertyChanged(string propertyName = null)
        {
            base.NotifyPropertyChanged(propertyName);
            if (propertyName == nameof(SelectedItem))
            {
                SkipVisibility = NextVisibility = SelectedItem != FourthView;
                GotItVisibility = SelectedItem == FourthView;
            }
        }
    }
}