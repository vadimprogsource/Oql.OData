using Oql.Linq.Api.Syntax.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlSyntaxContext
    {
        IOqlBitwiseFormatter    BitwiseFormatter { get; }
        IOqlBooleanFormatter    BooleanFormatter { get; }
        IOqlComparisonFormatter ComparisonFormatter { get; }
        IOqlMathFormatter        MathFormatter { get; }
        IOqlTakeByClause Taken { get; set; }
        IOqlMethodCallEntry this[MethodCallExpression methodCall] { get; }
        IEnumerable<IOqlClause> Clauses { get; }

        IQueryBuilder CreateQueryBuilder();
    }
}
