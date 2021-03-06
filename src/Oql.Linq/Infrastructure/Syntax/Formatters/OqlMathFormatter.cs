﻿using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlMathFormatter : IOqlMathFormatter
    {
        public void FormatAdd(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendAdd();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatAddChecked(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendAdd();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatDivide(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendDivide();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatMultiply(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendMultiply();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatMultiplyAssignChecked(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            FormatMultiply(visitor, left, right);
        }

        public void FormatNegate(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendNegate().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }

        public void FormatNegateChecked(IOqlExpressionVisitor visitor, Expression operand)
        {
            FormatNegate(visitor, operand);
        }

        public void FormatSubtract(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendAdd();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatSubtractChecked(IOqlExpressionVisitor visitor, Expression left, Expression right)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(left);
            visitor.Query.AppendSubtract();
            visitor.Visit(right);
            visitor.Query.AppendEndExpression();
        }

        public void FormatUnaryPlus(IOqlExpressionVisitor visitor, Expression operand)
        {
            visitor.Query.AppendPlus().AppendBeginExpression();
            visitor.Visit(operand);
            visitor.Query.AppendEndExpression();
        }
    }
}
