using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax.Formatters
{
    public interface IOqlBooleanFormatter
    {
        void FormatAndAlso(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatOrElse(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatNot(IOqlExpressionVisitor visitor, Expression operand);
    }
}
