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
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendAndAlso();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatExclusiveOr(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendOrElse();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatNot(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendNot();
            visitor.Query.AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }

        public void FormatOr(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendOrElse();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }
    }
}
