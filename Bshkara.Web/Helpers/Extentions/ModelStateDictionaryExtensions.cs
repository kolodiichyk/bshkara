using System.Linq;
using System.Web.Mvc;

namespace Bshkara.Web.Extentions
{
    public static class ModelStateDictionaryExtensions
    {
        public static bool HasNotPropertyErrors(this ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.Values.Any(x => x.Errors.Count >= 1 && x.Value == null);
        }
    }
}