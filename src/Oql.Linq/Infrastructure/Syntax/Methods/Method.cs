using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax.Methods
{

    public class Method : IMethodInfo
    {

        public static MethodInfo GetKeyMethod(MethodInfo m)
        {
            if (m.IsGenericMethod)
            {
                return m.GetGenericMethodDefinition();
            }

            return m;
        }

        public MethodInfo GetMethodInfo()
        {
            return m_method_info;
        }


        private MethodInfo m_method_info;

        public Method(MethodInfo methodInfo)
        {
            m_method_info = GetKeyMethod(methodInfo);
        }


        public override bool Equals(object obj)
        {
            if (obj is Method)
            {
                return  (obj as Method).m_method_info == m_method_info;
            }

            if (obj is MethodCallExpression)
            {
                return m_method_info == GetKeyMethod((obj as MethodCallExpression).Method);
            }

            if (obj is MethodInfo)
            {
                return m_method_info == GetKeyMethod(obj as MethodInfo);
            }

            return false; 
        }


        public override int GetHashCode()
        {
            return  GetType().GetHashCode() ^  m_method_info.GetHashCode();
        }


        public IQueryable<T> Call<T>(IQueryable<T> left , LambdaExpression right)
        {
           return  left.Provider.CreateQuery<T>(Expression.Call(m_method_info.MakeGenericMethod(left.ElementType, right.Body.Type),left.Expression,right)); 
        }


        public Expression Call<T>(Expression left, LambdaExpression right)
        {
            return Expression.Call(m_method_info.MakeGenericMethod(typeof(T), right.Body.Type), left, right);
        }

    }

    public class Method<T> : Method
    {
        public Method(Expression<Action<T>> methodCall) : base((methodCall.Body as MethodCallExpression).Method){}
    }
}
