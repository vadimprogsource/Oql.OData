using Oql.Linq.Api;
using System;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlExpressionVisitor : ExpressionVisitor, IOqlExpressionVisitor
    {

        private IOqlSyntaxProvider m_syntax_provider;
        private IQueryBuilder      m_query_builder;


        public OqlExpressionVisitor(IOqlSyntaxProvider provider , IQueryBuilder queryBuilder)
        {
            m_syntax_provider      = provider;
            m_query_builder = queryBuilder;
        }

        public IQueryBuilder QueryBuilder
        {
            get
            {
                return m_query_builder;
            }
        }

        public IOqlSyntaxProvider SyntaxProvider
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
                 case ExpressionType.Equal             : m_syntax_provider.GetComparison().FormatEqual   (this, node.Left, node.Right); break;
                 case ExpressionType.NotEqual          : m_syntax_provider.GetComparison().FormatNotEqual(this, node.Left, node.Right); break;
                 case ExpressionType.GreaterThan       : m_syntax_provider.GetComparison().FormatGreaterThan(this, node.Left, node.Right); break;
                 case ExpressionType.GreaterThanOrEqual: m_syntax_provider.GetComparison().FormatGreaterThanOrEqual(this, node.Left, node.Right); break;
                 case ExpressionType.LessThan          : m_syntax_provider.GetComparison().FormatLessThan(this, node.Left, node.Right);  break;
                 case ExpressionType.LessThanOrEqual   : m_syntax_provider.GetComparison().FormatLessThanOrEqual(this, node.Left, node.Right); break;
                #endregion

                #region Boolean
                 case ExpressionType.AndAlso : m_syntax_provider.GetBoolean().FormatAndAlso(this,node.Left, node.Right); break;
                 case ExpressionType.OrElse : m_syntax_provider.GetBoolean().FormatOrElse (this,node.Left, node.Right); break;
               #endregion

                #region Bitwise
                 case ExpressionType.And        : m_syntax_provider.GetBitwise().FormatAnd         (this, node.Left, node.Right); break;
                 case ExpressionType.Or         : m_syntax_provider.GetBitwise().FormatOr          (this, node.Left, node.Right); break;
                 case ExpressionType.ExclusiveOr: m_syntax_provider.GetBitwise().FormatExclusiveOr (this, node.Left, node.Right); break;
                #endregion

                #region Math
                 case ExpressionType.Add                  : m_syntax_provider.GetMath().FormatAdd                  (this, node.Left, node.Right);break;
                 case ExpressionType.AddChecked           : m_syntax_provider.GetMath().FormatAddChecked           (this, node.Left, node.Right);break;
                 case ExpressionType.Subtract             : m_syntax_provider.GetMath().FormatSubtract             (this, node.Left, node.Right);break;
                 case ExpressionType.SubtractChecked      : m_syntax_provider.GetMath().FormatSubtractChecked      (this, node.Left, node.Right);break;
                 case ExpressionType.Multiply             : m_syntax_provider.GetMath().FormatMultiply             (this, node.Left, node.Right);break;
                 case ExpressionType.MultiplyAssignChecked: m_syntax_provider.GetMath().FormatMultiplyAssignChecked(this, node.Left, node.Right);break;
                 case ExpressionType.Divide               : m_syntax_provider.GetMath().FormatDivide               (this, node.Left, node.Right);break;
               #endregion

            }


            return node;
        }


        protected override Expression VisitUnary(UnaryExpression node)
        {

            switch (node.NodeType)
            {
                case ExpressionType.Negate: m_syntax_provider.GetMath().FormatNegate(this, node.Operand); break;
                case ExpressionType.NegateChecked: m_syntax_provider.GetMath().FormatNegate(this, node.Operand); break;
                case ExpressionType.Not: m_syntax_provider.GetBoolean().FormatNot(this, node.Operand); break;
                case ExpressionType.Quote: return Visit(node.Operand);
                case ExpressionType.UnaryPlus: m_syntax_provider.GetMath().FormatUnaryPlus(this, node.Operand); break;

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
                        QueryBuilder.AppendPropertyOrFieldPathSeparator();
                    }

                    hasSeparator = true;
                    QueryBuilder.AppendMember(x.Member);

                }


                return node;
            }

            QueryBuilder.AppendValue(node.Type, Expression.Lambda(node).Compile().DynamicInvoke());
            return node;
        }


        public virtual void VisitSearchPattern(bool hasBeginWildCard, Expression expression, bool hasEndWildCard)
        {

            object patternValue;


            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                if (expression.IsPropertyOrField())
                {
                    QueryBuilder.AppendMember((expression as MemberExpression).Member);
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


            QueryBuilder.AppendLikePattern(hasBeginWildCard, patternValue, hasEndWildCard);
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


            QueryBuilder.AppendValue(node.Type, node.Value);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {

            IOqlMethodCallEntry methodCall = m_syntax_provider.GetMethodCall(node);

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


            QueryBuilder.AppendBeginArray();

            int len = node.Expressions.Count;

            if (len > 0)
            {
                Visit(node.Expressions.First());

                for (int i = 1; i < len; i++)
                {
                    QueryBuilder.AppendArrayElementSeparator();
                    Visit(node.Expressions[i]);
                }
            }

            QueryBuilder.AppendEndArray();
            return node;
        }



        protected override Expression VisitNew(NewExpression node)
        {


            for (int i = 0; i < node.Arguments.Count; i++)
            {
                if (i > 0)
                {
                    QueryBuilder.AppendExpressionSeparator();
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
                    QueryBuilder.AppendExpressionSeparator();
                }

                Visit((node.Bindings[i] as MemberAssignment).Expression);
            }

            return node;
        }


        public override string ToString()
        {
            Build();
            return QueryBuilder.ToString();
        }


        public IQueryBuilder Build()
        {
            QueryBuilder.Clear();

            foreach (IOqlClause clause in m_syntax_provider.GetOrderedClauses())
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

