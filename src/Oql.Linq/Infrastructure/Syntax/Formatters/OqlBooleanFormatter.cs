using System.Linq.Expressions;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlBooleanFormatter : IOqlBooleanFormatter
    {
        public void FormatAndAlso(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendAndAlso();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();

        }

        public void FormatNot(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.QueryBuilder.AppendNot().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatOrElse(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {

            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendOrElse();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();

        }
    }
}
