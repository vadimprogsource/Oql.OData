using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax.Formatters
{
    public interface IOqlBitwiseFormatter
    {
        void FormatAnd(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatOr(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatExclusiveOr(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatNot(IOqlExpressionVisitor visitor, Expression operand);
    }
}
