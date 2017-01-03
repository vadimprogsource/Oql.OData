using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IMethod
    {
        MethodInfo GetMethodInfo();
        MethodInfo GetBaseMethod();


        Expression Call<T>(Expression operand);
       
        Expression Call<T, V>(Expression left, LambdaExpression right);
    }
}
