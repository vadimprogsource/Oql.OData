using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlExpressionVisitor
    {
        Type SourceType { get;  }

        IOqlSyntaxContext  Context { get; }
        IQueryBuilder       Query  { get; }
        Expression Visit(Expression expression);
        void VisitSearchPattern(bool hasBeginWildCard, Expression expression, bool hasEndWildCard);
        IOqlExpressionVisitor VisitSubQuery(Expression expression);

        IOqlExpressionVisitor ExecuteVisit(Expression expression);
    }

}
