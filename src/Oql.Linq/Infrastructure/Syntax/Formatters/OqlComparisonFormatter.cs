using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlComparisonFormatter : IOqlComparisonFormatter
    {
        public void FormatEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendEqual();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatLessThan(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendLessThan();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatGreaterThan(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendGreaterThan();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatGreaterThanOrEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendGreaterThanOrEqual();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatLessThanOrEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendLessThanOrEqual();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatNotEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendNotEqual();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }
    }
}
