using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlComparisonFormatter : IOqlComparisonFormatter
    {
        public void FormatEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendEqual();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatLessThan(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendLessThan();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatGreaterThan(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendGreaterThan();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatGreaterThanOrEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendGreaterThanOrEqual();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatLessThanOrEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendLessThanOrEqual();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatNotEqual(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendNotEqual();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }
    }
}
