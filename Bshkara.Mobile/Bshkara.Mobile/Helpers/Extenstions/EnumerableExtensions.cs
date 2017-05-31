using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bashkra.ApiClient.Models.Base;
using Bshkara.Mobile.ViewModels.Base;

namespace Bshkara.Mobile.Helpers.Extenstions
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            return new ObservableCollection<T>(elements);
        }

        public static ObservableCollection<Selectable<T>> ToObservableSelectablCollection<T>(
            this IEnumerable<T> elements, IEnumerable<Guid> selected) where T : ApiBaseModel
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            return
                new ObservableCollection<Selectable<T>>(
                    elements.Select(item => new Selectable<T>(item) {IsSelected = selected.Contains(item.Id)}));
        }
    }
}