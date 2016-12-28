using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oql.Linq.Api;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlBitwiseFormatter : IOqlBitwiseFormatter
    {
        public void FormatAnd(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendAndAlso();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatExclusiveOr(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendOrElse();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatNot(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.QueryBuilder.AppendNot();
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(operand);
            visitor.QueryBuilder.AppendEndExpression();
        }

        public void FormatOr(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(left);
            visitor.QueryBuilder.AppendOrElse();
            visitor.Visit(right);
            visitor.QueryBuilder.AppendEndExpression();
        }
    }
}
