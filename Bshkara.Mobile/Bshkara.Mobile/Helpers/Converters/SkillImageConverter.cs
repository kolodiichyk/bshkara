using System;
using System.Collections.Generic;
using System.Globalization;
using Bashkra.ApiClient.Models;
using Xamarin.Forms;

namespace Bshkara.Mobile.Helpers.Converters
{
    public class SkillUnactivedImageConverter : IValueConverter
    {
        private readonly Dictionary<string, string> _skillLocalImageDictionary = new Dictionary<string, string>
        {
            {"Adult Care", "skill_adult_care.png"},
            {"Babysitting", "babysitting.png"},
            {"Cocking", "skill_cocking.png"},
            {"Decorating", "skill_decorating.png"},
            {"Driving", "skill_driving.png"},
            {"House cleaning", "skill_hause_cleaning.png"},
            {"Pet Care", "skill_pet_care.png"},
            {"Washing", "skill_washing.png"}
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var skill = value as ApiSkill;
            if (skill == null)
                return null;

            var image = skill.Icon;
            _skillLocalImageDictionary.TryGetValue(skill.Name, out image);

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}