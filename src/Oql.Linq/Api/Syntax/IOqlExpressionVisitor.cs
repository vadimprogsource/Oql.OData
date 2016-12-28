using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlExpressionVisitor
    {
        Type BaseEntityType { get;  }
        Type ResulType      { get; set; }

        IOqlSyntaxProvider  SyntaxProvider { get; }
        IQueryBuilder        QueryBuilder { get; }
        Expression Visit(Expression expression);
        void VisitSearchPattern(bool hasBeginWildCard, Expression expression, bool hasEndWildCard);

        IQueryBuilder VisitAndBuild(Expression expression);
    }

}
