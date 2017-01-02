using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlClause
    {
        void ProcessMethodCall(IOqlSyntaxContext callContext , MethodCallExpression methodCall);
        void VisitTo(IOqlExpressionVisitor visitor);
    }





    
}
