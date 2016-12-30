using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlInsertClause : OqlBaseClause
    {
        public override void AddMethodCall(MethodCallExpression methodCall)
        {
            throw new NotImplementedException();
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
