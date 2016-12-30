using System.Linq.Expressions;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlBooleanFormatter : IOqlBooleanFormatter
    {
        public void FormatAndAlso(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendAndAlso();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();

        }

        public void FormatNot(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendNot().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }

        public void FormatOrElse(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {

            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendOrElse();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();

        }
    }
}
