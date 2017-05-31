using System.Linq.Expressions;
using System.Reflection;

namespace Bshkara.Mobile.Helpers.Extenstions
{
    public static class ExpressionExtensions
    {
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression) expression;

            MemberExpression memberExpression;
            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = (MemberExpression) unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression) lambda.Body;
            }

            return memberExpression.Member;
        }
    }
}