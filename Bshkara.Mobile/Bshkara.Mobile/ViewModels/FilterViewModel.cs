using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bshkara.Mobile.Helpers;
using Bshkara.Mobile.Helpers.Extenstions;
using Bshkara.Mobile.Services.WebService;
using Bshkara.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace Bshkara.Mobile.ViewModels
{
    public class FilterViewModel : BaseViewModel, IDisposable
    {
        public const string MessageKeyMaidFilterChanged = "MaidFilterChanged";

        private readonly IWebService _webService;

        private bool _initFilterControls;

        private IEnumerable<Guid> _selectedLangueges;

        private IEnumerable<Guid> _selectedSkills;

        public FilterViewModel(IWebService webService)
        {
            MessagingCenter.Subscribe<CitiesViewModel, ApiCity>(this, CitiesViewModel.MessageForFilterKey,
                   OnCitiesSelect);
            MessagingCenter.Subscribe<NationalitiesViewModel, ApiNationality>(this, NationalitiesViewModel.MessageKey,
                OnNationalitiesSelect);
            _webService = webService;
            _initFilterControls = true;
        }

        public int MaxSalaryValue { get; set; } = 10000;

        public int MinSalaryValue { get; set; } = 0;

        public int MaxExperienceValue { get; set; } = 100;

        public int MinExperienceValue { get; set; } = 0;

        public int MaxAgeRangeValue { get; set; } = 100;

        public int MinAgeRangeValue { get; set; } = 16;

        public ObservableCollection<Selectable<ApiSkill>> Skills { get; set; }

        public ISelectable<ApiSkill> SelectedSkill { get; set; }

        public ApiNationality Nationality { get; set; }

        public int MaxSalary { get; set; }

        public int MinSalary { get; set; }

        public int MaxExperience { get; set; }

        public int MinExperience { get; set; }

        public int MaxAgeRange { get; set; }

        public int MinAgeRange { get; set; }

        public bool ShowOnlyWithPhoto { get; set; }

        public ObservableCollection<Selectable<ApiLanguage>> SelectedLanguages { get; set; }

        

        public ICommand SelectNationalityCommand => new Command(SelectNationality);

        public ICommand SelectCityCommand => new Command(SelectCity);

        public ICommand ShowMaidsWithPhotosOnlyCommand => new Command(ShowMaidsWithPhotosOnly);

        public ICommand ApplyFilterCommand => new Command(ApplyFilter);

        public ICommand ResetCommand => new Command(Reset);


        public void Dispose()
        {
            MessagingCenter.Unsubscribe<NationalitiesViewModel>(this, NationalitiesViewModel.MessageKey);
            MessagingCenter.Unsubscribe<CitiesViewModel>(this, CitiesViewModel.MessageForFilterKey);
        }

        private void Reset()
        {
            Settings.MaidFilter = new MaidsArgs();
            MessagingCenter.Send(this, MessageKeyMaidFilterChanged);
            CloseCommand.Execute(null);
        }

        public override async void OnViewAppearing()
        {
            base.OnViewAppearing();

            if (!_initFilterControls)
                return;

            var filter = Settings.MaidFilter;

            MaxSalary = (int?) filter.MaxSalary ?? MaxSalaryValue;
            MinSalary = (int?) filter.MinSalary ?? MinSalaryValue;
            MaxAgeRange = (int?) filter.MaxAge ?? MaxAgeRangeValue;
            MinAgeRange = (int?) filter.MinAge ?? MinAgeRangeValue;
            MaxExperience = (int?) filter.MaxYearsOfExperience ?? MaxExperienceValue;
            MinExperience = (int?) filter.MinYearsOfExperience ?? MinExperienceValue;

            ShowOnlyWithPhoto = filter.OnlyWithPhotos ?? false;

            Nationality = Settings.NationalityFilter;
            City = Settings.CityFilter;
            Skills = new ObservableCollection<Selectable<ApiSkill>>(Settings.SkillsFilter);
            SelectedLanguages = new ObservableCollection<Selectable<ApiLanguage>>(Settings.LanguagesFilter);

            Nationality = filter.Nationality == null
                ? null
                : (await _webService.GetNationalitiesAsync(new NationalitiesArgs {Id = filter.Nationality}))
                    .FirstOrDefault();
            City = filter.City == null
                ? null
                : (await _webService.GetCitiesAsync(new CitiesArgs {Id = filter.City}))
                    .FirstOrDefault();

            _selectedSkills = filter.Skills;
            _selectedLangueges = filter.Languages;

            GetSkills();
            GetLangueges();

            _initFilterControls = false;
        }

        private void ApplyFilter()
        {
            Settings.MaidFilter = new MaidsArgs
            {
                Languages = SelectedLanguages.Where(t => t.IsSelected).Select(t => t.Item.Id).ToList(),
                Nationality = Nationality?.Id,
                City = City?.Id,
                Skills = Skills.Where(t => t.IsSelected).Select(t => t.Item.Id).ToList(),
                MaxSalary = MaxSalary,
                MinSalary = MinSalary,
                MaxYearsOfExperience = MaxExperience,
                MinYearsOfExperience = MinExperience,
                MaxAge = MaxAgeRange,
                MinAge = MinAgeRange,
                OnlyWithPhotos = ShowOnlyWithPhoto
            };

            Settings.CityFilter = City;
            Settings.NationalityFilter = Nationality;
            Settings.LanguagesFilter = SelectedLanguages;
            Settings.SkillsFilter = Skills;

            MessagingCenter.Send(this, MessageKeyMaidFilterChanged);
            CloseCommand.Execute(null);
        }

        protected override void OnCitiesSelect(CitiesViewModel sender, ApiCity city)
        {
            City = city;
        }

        private void ShowMaidsWithPhotosOnly()
        {
            ShowOnlyWithPhoto = !ShowOnlyWithPhoto;
        }

        private async void GetSkills()
        {
            Skills = (await _webService.GetSkillsAsync()).ToObservableSelectablCollection(_selectedSkills);
        }

        private async void GetLangueges()
        {
            SelectedLanguages =
                (await _webService.GetLanguagesAsync()).ToObservableSelectablCollection(_selectedLangueges);
        }

        private async void SelectCity()
        {
            await Navigation.For<CitiesViewModel>()
                .WithParam(t => t.MessageKey, CitiesViewModel.MessageForFilterKey)
                .NavigateModal();
        }

        private void OnNationalitiesSelect(NationalitiesViewModel sender, ApiNationality nationality)
        {
            Nationality = nationality;
        }

        private async void SelectNationality()
        {
            await Navigation.For<NationalitiesViewModel>().NavigateModal();
        }

        protected override void NotifyPropertyChanged(string propertyName = null)
        {
            base.NotifyPropertyChanged(propertyName);
            if ((propertyName == nameof(SelectedSkill)) && (SelectedSkill != null))
                SelectedSkill.IsSelected = !SelectedSkill.IsSelected;
        }
    }
}