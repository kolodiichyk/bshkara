using System;
using System.Linq;

namespace Bshkara.Web.Extentions
{
    public static class ExceptionExtentions
    {
        public static string GetExceptionDetailsAsString(this Exception exception)
        {
            var properties = exception.GetType().GetProperties();

            var fields = properties
                .Select(property => new
                {
                    property.Name,
                    Value = property.GetValue(exception, null)
                })
                .Select(x => string.Format(
                    "{0} = {1}",
                    x.Name,
                    x.Value != null ? x.Value.ToString() : string.Empty
                    ));

            return string.Join("\n", fields);
        }
    }
}