using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlMathFormatter : IOqlMathFormatter
    {
        public void FormatAdd(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendAdd();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatAddChecked(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendAdd();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatDivide(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendDivide();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatMultiply(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendMultiply();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatMultiplyAssignChecked(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            FormatMultiply(visitor, left, right);
        }

        public void FormatNegate(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.QueryBuilder.AppendNegate().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatNegateChecked(IOqlExpressionVisitor visitor, Expression operand)
        {
            FormatNegate(visitor, operand);
        }

        public void FormatSubtract(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendAdd();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatSubtractChecked(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendSubtract();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatUnaryPlus(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.QueryBuilder.AppendPlus().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.QueryBuilder.AppendEndExpression();
        }
    }
}
