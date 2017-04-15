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


        public static MemberInfo GetMemberInfo(this Expression @this)
        {
            return (@this.GetInnerExpression() as MemberExpression).Member;
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


        public static Expression GetArgument(this MethodCallExpression @this,int index)
        {
            return @this.Arguments[index].GetInnerExpression();
        }


        public static IEnumerable<IExpression> GetIterator(this IExpression @this)
        {
            for (IExpression x = @this; x != null; x = x.Expression)
            {
                yield return x;
            }
        }

        public static MethodInfo GetBaseMethod(this MethodInfo @this)
        {
            return @this.IsGenericMethod ? @this.GetGenericMethodDefinition() : @this;
        }

       


        public static bool IsCalled(this MethodCallExpression @this, string methodName)
        {
            return string.Compare(@this.Method.Name, methodName, false)==0;
        }

        public static bool IsCalled(this MethodCallExpression @this, IMethod method)
        {
            return ReferenceEquals(@this.Method.GetBaseMethod() , method.GetMethodInfo().GetBaseMethod());
        }

        public static bool IsCalledOr(this MethodCallExpression @this, params IMethod[] methods)
        {
            foreach (IMethod m in methods)
            {
                if (@this.IsCalled(m)) return true;
            }

            return false;
        }


    }
}
