using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax.Formatters
{
    public interface IOqlMethodCallFormatter
    {
        void FormatMethodCall(IOqlExpressionVisitor visitor , MethodCallExpression method);
    }
}
