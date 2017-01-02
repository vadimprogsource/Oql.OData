using Oql.Linq.Api.Syntax.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class AggregateFormatter : IAggregateFormatter
    {
        public void FormatAverage(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendAvg().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();

        }

        public void FormatCount(IOqlExpressionVisitor visitor)
        {
            visitor.Query.AppendCount().AppendBeginExpression().AppendMultiply().AppendEndExpression();
        }

        public void FormatCount(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendAvg().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }

        public void FormatMax(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendMax().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }

        public void FormatMin(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendMin().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }

        public void FormatSum(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendSum().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }
    }
}
