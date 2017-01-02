using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlFromClause : OqlBaseClause 
    {
    
        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            visitor.Query.AppendFrom().AppendType(visitor.SourceType);
        }
    }
}
