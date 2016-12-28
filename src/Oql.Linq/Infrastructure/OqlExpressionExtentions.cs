using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure
{
    public static class OqlExpressionExtentions
    {
        public static bool IsPropertyOrField(this Expression @this)
        {
            for (Expression x = @this; x != null;)
            {
                if (x.NodeType == ExpressionType.MemberAccess)
                {
                    x = (x as MemberExpression).Expression;
                    continue;
                }

                if (x.NodeType == ExpressionType.Parameter)
                {
                    return true;
                }
            }

            return false;

        }


        public static IEnumerable<MemberExpression> GetPathIterator(this MemberExpression @this)
        {
            for (MemberExpression x = @this; x != null; x = x.Expression as MemberExpression)
            {
                yield return x;
            }

        }


        public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> @this)
        {
            return (@this.Body as MethodCallExpression).Method;
        }


        public static object GetValue(this Expression @this)
        {
            if (@this.NodeType == ExpressionType.Constant)
            {
                return (@this as ConstantExpression).Value;
            }

            return Expression.Lambda(@this).Compile().DynamicInvoke();
        }


        public static Expression GetInnerExpression(this Expression @this)
        {
            if (@this.NodeType == ExpressionType.Quote)
            {
                @this = (@this as UnaryExpression).Operand;
            }

            if (@this.NodeType == ExpressionType.Lambda)
            {
                @this = (@this as LambdaExpression).Body;
            }

            return @this;
        }


        public static IEnumerable<IExpression> GetIterator(this IExpression @this)
        {
            for (IExpression x = @this; x != null; x = x.Expression)
            {
                yield return x;
            }
        }
    }
}
