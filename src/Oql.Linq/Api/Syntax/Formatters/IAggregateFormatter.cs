using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax.Formatters
{
    public interface IAggregateFormatter
    {
        void FormatMin(IOqlExpressionVisitor visitor, Expression operand);
        void FormatMax(IOqlExpressionVisitor visitor, Expression operand);
        void FormatAverage(IOqlExpressionVisitor visitor, Expression operand);
        void FormatSum(IOqlExpressionVisitor visitor, Expression operand);
        void FormatCount(IOqlExpressionVisitor visitor, Expression operand);
        void FormatCount(IOqlExpressionVisitor visitor);

    }
}
