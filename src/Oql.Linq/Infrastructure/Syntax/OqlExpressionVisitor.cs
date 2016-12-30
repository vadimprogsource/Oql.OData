using Oql.Linq.Api;
using System;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlExpressionVisitor : ExpressionVisitor, IOqlExpressionVisitor
    {

        private IOqlSyntaxContext m_syntax_provider;
        private IQueryBuilder      m_query_builder;


        public OqlExpressionVisitor(IOqlSyntaxContext provider)
        {
            m_syntax_provider = provider;
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
                return m_syntax_provider;
            }
        }

        public Type BaseEntityType { get; private set; }
        public Type ResulType      { get; set; }

       
        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
               #region Comparison
                 case ExpressionType.Equal             : m_syntax_provider.ComparisonFormatter.FormatEqual   (this, node.Left, node.Right); break;
                 case ExpressionType.NotEqual          : m_syntax_provider.ComparisonFormatter.FormatNotEqual(this, node.Left, node.Right); break;
                 case ExpressionType.GreaterThan       : m_syntax_provider.ComparisonFormatter.FormatGreaterThan(this, node.Left, node.Right); break;
                 case ExpressionType.GreaterThanOrEqual: m_syntax_provider.ComparisonFormatter.FormatGreaterThanOrEqual(this, node.Left, node.Right); break;
                 case ExpressionType.LessThan          : m_syntax_provider.ComparisonFormatter.FormatLessThan(this, node.Left, node.Right);  break;
                 case ExpressionType.LessThanOrEqual   : m_syntax_provider.ComparisonFormatter.FormatLessThanOrEqual(this, node.Left, node.Right); break;
                #endregion

                #region Boolean
                 case ExpressionType.AndAlso : m_syntax_provider.BooleanFormatter.FormatAndAlso(this,node.Left, node.Right); break;
                 case ExpressionType.OrElse : m_syntax_provider.BooleanFormatter.FormatOrElse (this,node.Left, node.Right); break;
               #endregion

                #region Bitwise
                 case ExpressionType.And        : m_syntax_provider.BitwiseFormatter.FormatAnd         (this, node.Left, node.Right); break;
                 case ExpressionType.Or         : m_syntax_provider.BitwiseFormatter.FormatOr          (this, node.Left, node.Right); break;
                 case ExpressionType.ExclusiveOr: m_syntax_provider.BitwiseFormatter.FormatExclusiveOr (this, node.Left, node.Right); break;
                #endregion

                #region Math
                 case ExpressionType.Add                  : m_syntax_provider.MathFormatter.FormatAdd                  (this, node.Left, node.Right);break;
                 case ExpressionType.AddChecked           : m_syntax_provider.MathFormatter.FormatAddChecked           (this, node.Left, node.Right);break;
                 case ExpressionType.Subtract             : m_syntax_provider.MathFormatter.FormatSubtract             (this, node.Left, node.Right);break;
                 case ExpressionType.SubtractChecked      : m_syntax_provider.MathFormatter.FormatSubtractChecked      (this, node.Left, node.Right);break;
                 case ExpressionType.Multiply             : m_syntax_provider.MathFormatter.FormatMultiply             (this, node.Left, node.Right);break;
                 case ExpressionType.MultiplyAssignChecked: m_syntax_provider.MathFormatter.FormatMultiplyAssignChecked(this, node.Left, node.Right);break;
                 case ExpressionType.Divide               : m_syntax_provider.MathFormatter.FormatDivide               (this, node.Left, node.Right);break;
               #endregion

            }


            return node;
        }


        protected override Expression VisitUnary(UnaryExpression node)
        {

            switch (node.NodeType)
            {
                case ExpressionType.Negate: m_syntax_provider.MathFormatter.FormatNegate(this, node.Operand); break;
                case ExpressionType.NegateChecked: m_syntax_provider.MathFormatter.FormatNegate(this, node.Operand); break;
                case ExpressionType.Not: m_syntax_provider.BooleanFormatter.FormatNot(this, node.Operand); break;
                case ExpressionType.Quote: return Visit(node.Operand);
                case ExpressionType.UnaryPlus: m_syntax_provider.MathFormatter.FormatUnaryPlus(this, node.Operand); break;

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
                BaseEntityType = (value as IQueryable).ElementType;
                ResulType      = BaseEntityType;
                return node;
            }


            Query.AppendValue(node.Type, node.Value);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {

            IOqlMethodCallEntry methodCall = m_syntax_provider[node];

            if (methodCall == null)
            {
                return VisitMethodCall(node);
            }


            if (methodCall.Clause != null)
            {
                methodCall.Clause.AddMethodCall(node);
                return Visit(node.Arguments[0]);
            }

            if (methodCall.Formatter != null)
            {
                methodCall.Formatter.FormatMethodCall(this,node);
            }

            return node;

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


            for (int i = 0; i < node.Arguments.Count; i++)
            {
                if (i > 0)
                {
                    Query.AppendExpressionSeparator();
                }

                Visit(node.Arguments[i]);
            }

            return node;
        }


        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            ResulType = node.NewExpression.Type; 

            if (node.NewExpression.Arguments.Count > 0)
            {
                return VisitNew(node.NewExpression);
            }

            for (int i = 0; i < node.Bindings.Count; i++)
            {
                if (i > 0)
                {
                    Query.AppendExpressionSeparator();
                }

                Visit((node.Bindings[i] as MemberAssignment).Expression);
            }

            return node;
        }


        public override string ToString()
        {
            Build();
            return Query.ToString();
        }


        public IQueryBuilder Build()
        {
            Query.Clear();

            foreach (IOqlClause clause in m_syntax_provider.Clauses)
            {
                clause.VisitTo(this);
            }

            return m_query_builder;

           
        }

        public IQueryBuilder VisitAndBuild(Expression expression)
        {
            Visit(expression);
            return Build();
        }
    }


}

