using System;
using System.Reflection;

namespace Bshkara.Web.Areas.ApiHelp.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}