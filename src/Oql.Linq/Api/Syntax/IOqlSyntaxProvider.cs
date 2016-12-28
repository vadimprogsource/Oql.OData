using Oql.Linq.Api.Syntax.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlSyntaxProvider
    {
       IOqlBitwiseFormatter    GetBitwise(); 
       IOqlBooleanFormatter    GetBoolean();
       IOqlComparisonFormatter GetComparison();
       IOqlMathFormatter       GetMath();

        IOqlTakeByClause GetTake();
        IOqlMethodCallEntry GetMethodCall(MethodCallExpression methodCall);
        IEnumerable<IOqlClause> GetOrderedClauses();
    }
}
