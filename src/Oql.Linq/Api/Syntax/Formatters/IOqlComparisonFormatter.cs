using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax.Formatters
{
    public interface IOqlComparisonFormatter
    {
        void FormatEqual(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatNotEqual(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatGreaterThan(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatGreaterThanOrEqual(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatLessThan(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatLessThanOrEqual(IOqlExpressionVisitor visitor, Expression left, Expression right);
    }
}
