using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bshkara.Mobile.Helpers.Extenstions
{
    public static class PropertyExtention
    {
        public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName)
        {
            var typeInfo = type.GetTypeInfo();
            var typeList = new List<Type> {type};

            if (typeInfo.IsInterface)
            {
                typeList.AddRange(typeInfo.ImplementedInterfaces);
            }

            return typeList
                .Select(interfaceType => interfaceType.GetRuntimeProperty(propertyName))
                .FirstOrDefault(property => property != null);
        }
    }
}