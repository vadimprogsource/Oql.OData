using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax.Formatters
{
   public  interface IOqlMathFormatter
    {
        void FormatAdd(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatAddChecked(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatSubtract(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatSubtractChecked(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatMultiply(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatMultiplyAssignChecked(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatDivide(IOqlExpressionVisitor visitor, Expression left, Expression right);
        void FormatNegate(IOqlExpressionVisitor visitor, Expression operand);
        void FormatNegateChecked(IOqlExpressionVisitor visitor, Expression operand);
        void FormatUnaryPlus(IOqlExpressionVisitor visitor, Expression operand);
    }
}
