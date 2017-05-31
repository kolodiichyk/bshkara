using Bshkara.Core.Base;

namespace Bshkara.Core.Helper
{
    public static class LocalizedHelper
    {
        public static string GetDefault(ILocalizedProperty value)
        {
            switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
            {
                case "en":
                    return value.En;
                case "ar":
                    return value.Ar;
                default:
                    return value.En;
            }
        }
    }
}