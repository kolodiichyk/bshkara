using System;
using System.Windows.Input;
using Bashkra.ApiClient.Models;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Helpers.Extenstions;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.ViewModels
{
    public class BaseViewModel : ViewModel, IDisposable
    {
        public ApiCity City { get; set; }

        public BaseViewModel()
        {
            City = Settings.UserLocation;
            MessagingCenter.Subscribe<CitiesViewModel, ApiCity>(this, CitiesViewModel.MessageForUserLocationKey,
                OnCitiesSelect);
        }

        protected virtual void OnCitiesSelect(CitiesViewModel sender, ApiCity city)
        {
            Settings.UserLocation = city;
            City = city;
        }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public ICommand ChangeCityCommand => new Command(ChangeCityAsync);

        private async void ChangeCityAsync()
        {
            await Navigation.For<CitiesViewModel>()
                .WithParam(t => t.MessageKey, CitiesViewModel.MessageForUserLocationKey)
                .WithParam(t=>t.HideAnyButton, true)
                .NavigateModal();
        }

        public ICommand GoBackCommand => new Command(GoBack);

        public ICommand CloseCommand => new Command(Close);

        private void Close()
        {
            Navigation.PopModalAsync();
        }

        private void GoBack(object param)
        {
            Navigation.PopAsync();
        }

        public void Dispose()
        {
            MessagingCenter.Unsubscribe<CitiesViewModel>(this, CitiesViewModel.MessageForUserLocationKey);
        }
    }
}