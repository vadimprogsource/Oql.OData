using Oql.Linq.Api;
using System;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlExpressionVisitor : ExpressionVisitor, IOqlExpressionVisitor
    {

        private IOqlSyntaxContext m_syntax_context;
        private IQueryBuilder     m_query_builder ;


        public OqlExpressionVisitor(IOqlSyntaxContext provider)
        {
            m_syntax_context = provider;
            m_query_builder   = provider.CreateQueryBuilder();
        }

        public IQueryBuilder Query
        {
            get
            {
                return m_query_builder;
            }
        }

        public IOqlSyntaxContext Context
        {
            get
            {
                return m_syntax_context;
            }
        }

        public Type SourceType { get; private set; }

       
        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
               #region Comparison
                 case ExpressionType.Equal             : m_syntax_context.ComparisonFormatter.FormatEqual   (this, node.Left, node.Right); break;
                 case ExpressionType.NotEqual          : m_syntax_context.ComparisonFormatter.FormatNotEqual(this, node.Left, node.Right); break;
                 case ExpressionType.GreaterThan       : m_syntax_context.ComparisonFormatter.FormatGreaterThan(this, node.Left, node.Right); break;
                 case ExpressionType.GreaterThanOrEqual: m_syntax_context.ComparisonFormatter.FormatGreaterThanOrEqual(this, node.Left, node.Right); break;
                 case ExpressionType.LessThan          : m_syntax_context.ComparisonFormatter.FormatLessThan(this, node.Left, node.Right);  break;
                 case ExpressionType.LessThanOrEqual   : m_syntax_context.ComparisonFormatter.FormatLessThanOrEqual(this, node.Left, node.Right); break;
                #endregion

                #region Boolean
                 case ExpressionType.AndAlso : m_syntax_context.BooleanFormatter.FormatAndAlso(this,node.Left, node.Right); break;
                 case ExpressionType.OrElse : m_syntax_context.BooleanFormatter.FormatOrElse (this,node.Left, node.Right); break;
               #endregion

                #region Bitwise
                 case ExpressionType.And        : m_syntax_context.BitwiseFormatter.FormatAnd         (this, node.Left, node.Right); break;
                 case ExpressionType.Or         : m_syntax_context.BitwiseFormatter.FormatOr          (this, node.Left, node.Right); break;
                 case ExpressionType.ExclusiveOr: m_syntax_context.BitwiseFormatter.FormatExclusiveOr (this, node.Left, node.Right); break;
                #endregion

                #region Math
                 case ExpressionType.Add                  : m_syntax_context.MathFormatter.FormatAdd                  (this, node.Left, node.Right);break;
                 case ExpressionType.AddChecked           : m_syntax_context.MathFormatter.FormatAddChecked           (this, node.Left, node.Right);break;
                 case ExpressionType.Subtract             : m_syntax_context.MathFormatter.FormatSubtract             (this, node.Left, node.Right);break;
                 case ExpressionType.SubtractChecked      : m_syntax_context.MathFormatter.FormatSubtractChecked      (this, node.Left, node.Right);break;
                 case ExpressionType.Multiply             : m_syntax_context.MathFormatter.FormatMultiply             (this, node.Left, node.Right);break;
                 case ExpressionType.MultiplyAssignChecked: m_syntax_context.MathFormatter.FormatMultiplyAssignChecked(this, node.Left, node.Right);break;
                 case ExpressionType.Divide               : m_syntax_context.MathFormatter.FormatDivide               (this, node.Left, node.Right);break;
               #endregion

            }


            return node;
        }


        protected override Expression VisitUnary(UnaryExpression node)
        {

            switch (node.NodeType)
            {
                case ExpressionType.Negate: m_syntax_context.MathFormatter.FormatNegate(this, node.Operand); break;
                case ExpressionType.NegateChecked: m_syntax_context.MathFormatter.FormatNegate(this, node.Operand); break;
                case ExpressionType.Not: m_syntax_context.BooleanFormatter.FormatNot(this, node.Operand); break;
                case ExpressionType.Quote: return Visit(node.Operand);
                case ExpressionType.UnaryPlus: m_syntax_context.MathFormatter.FormatUnaryPlus(this, node.Operand); break;

                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.TypeAs: return Visit(node.Operand);
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.IsPropertyOrField())
            {

                bool hasSeparator = false;

                foreach (MemberExpression x in node.GetPathIterator().Reverse())
                {
                    if (hasSeparator)
                    {
                        Query.AppendPropertyOrFieldPathSeparator();
                    }

                    hasSeparator = true;
                    Query.AppendMember(x.Member);

                }


                return node;
            }

            Query.AppendValue(node.Type, Expression.Lambda(node).Compile().DynamicInvoke());
            return node;
        }


        public virtual void VisitSearchPattern(bool hasBeginWildCard, Expression expression, bool hasEndWildCard)
        {

            object patternValue;


            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                if (expression.IsPropertyOrField())
                {
                    Query.AppendMember((expression as MemberExpression).Member);
                    return;
                }

                patternValue = Expression.Lambda(expression).Compile().DynamicInvoke();
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                patternValue = (expression as ConstantExpression).Value;
            }
            else
            {
                throw new NotSupportedException();
            }


            Query.AppendLikePattern(hasBeginWildCard, patternValue, hasEndWildCard);
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {

            object value = node.Value;

            if (value is IQueryable)
            {
                SourceType = (value as IQueryable).ElementType;
                m_syntax_context.CallResult.ResultType = SourceType;
                return node;
            }


            Query.AppendValue(node.Type, node.Value);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return m_syntax_context.ProcessMethodCall(this, node);
        }



        protected override Expression VisitNewArray(NewArrayExpression node)
        {


            Query.AppendBeginArray();

            int len = node.Expressions.Count;

            if (len > 0)
            {
                Visit(node.Expressions.First());

                for (int i = 1; i < len; i++)
                {
                    Query.AppendArrayElementSeparator();
                    Visit(node.Expressions[i]);
                }
            }

            Query.AppendEndArray();
            return node;
        }



        protected override Expression VisitNew(NewExpression node)
        {

            Visit(node.Arguments.First());

            foreach (Expression x in node.Arguments.Skip(1))
            {
                Query.AppendExpressionSeparator();
                Visit(x);
            }

            return node;
        }


        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            m_syntax_context.CallResult.ResultType = node.NewExpression.Type; 

            if (node.NewExpression.Arguments.Count > 0)
            {
                return VisitNew(node.NewExpression);
            }

            Visit(node.Bindings.OfType<MemberAssignment>().First().Expression);

            foreach (MemberAssignment x in node.Bindings.Skip(1))
            {
                m_query_builder.AppendExpressionSeparator();
                Visit(x.Expression);
            }

            return node;
        }


        public override string ToString()
        {
            MakeQuery();
            return Query.ToString();
        }


        protected IOqlExpressionVisitor MakeQuery()
        {
            m_query_builder.Clear();

            foreach (IOqlClause clause in m_syntax_context.Clauses)
            {
                clause.VisitTo(this);
            }
            return this;
        }

        public IOqlExpressionVisitor ExecuteVisit(Expression expression)
        {
            m_syntax_context.InitializeFor(expression);
            Visit(expression);
            return MakeQuery();
        }
    }


}

