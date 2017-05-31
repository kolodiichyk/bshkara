using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bshkara.Mobile.Helpers.Extenstions;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Helpers
{
    public class NavigateHelper<TViewModel> where TViewModel : ViewModel
    {
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();
        private ViewModelNavigation _navigationService;

        public NavigateHelper<TViewModel> WithParam<TValue>(Expression<Func<TViewModel, TValue>> property, TValue value)
        {
            if (value is ValueType || !ReferenceEquals(null, value))
            {
                _parameters[property.GetMemberInfo().Name] = value;
            }

            return this;
        }

        public NavigateHelper<TViewModel> AttachTo(ViewModelNavigation navigationService)
        {
            _navigationService = navigationService;
            return this;
        }

        public async Task Navigate(bool animated = true)
        {
            if (_navigationService == null)
            {
                throw new InvalidOperationException(
                    "Cannot navigate without attaching an ViewModelNavigation. Call AttachTo first.");
            }

            await _navigationService.PushAsync(new Action<TViewModel, Page>(ActivateAction), animated);
        }

        public async Task NavigateModal(bool animated = true)
        {
            if (_navigationService == null)
            {
                throw new InvalidOperationException(
                    "Cannot navigate without attaching an ViewModelNavigation. Call AttachTo first.");
            }

            await _navigationService.PushModalAsync(new Action<TViewModel, Page>(ActivateAction), animated);
        }

        private void ActivateAction(TViewModel model, Page page)
        {
            TryInjectParameters(model, _parameters);
        }

        private void TryInjectParameters(IViewModel viewModel, object parameter)
        {
            var viewModelType = viewModel.GetType();

            var dictionaryParameter = parameter as IDictionary<string, object>;
            if (dictionaryParameter != null)
            {
                foreach (var pair in dictionaryParameter)
                {
                    var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                    property?.SetValue(viewModel, pair.Value);
                }
            }
        }
    }
}