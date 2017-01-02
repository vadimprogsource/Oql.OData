using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Infrastructure.Syntax.Clauses;
using Oql.Linq.Infrastructure.Syntax.Methods;
using System.Collections.Generic;
using Oql.Linq.Api;
using Oql.Linq.Infrastructure.Syntax.Formatters;
using System;
using System.Linq.Expressions;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax
{
    public abstract class OqlSyntaxContext : MethodSet ,  IOqlSyntaxContext
    {

        public IAggregateFormatter AggregateFormatter { get; protected set; } = new AggregateFormatter();
       
        public IOqlBitwiseFormatter BitwiseFormatter { get; protected set; } = new OqlBitwiseFormatter();

        public IOqlBooleanFormatter BooleanFormatter { get; protected set; } = new OqlBooleanFormatter();

        public IOqlComparisonFormatter ComparisonFormatter { get; protected set; } = new OqlComparisonFormatter();
        public IOqlMathFormatter MathFormatter { get; protected set; } = new OqlMathFormatter();

 

        public OqlSyntaxContext()
        {
            Call<OqlEndsWithFormatter>  ();
            Call<OqlStartsWithFormatter>();
            Call<OqlContainsFormatter>  ();
        }

        public IEnumerable<IOqlClause> Clauses
        {
            get
            {
                return this;
            }
        }



        protected virtual IOqlSyntaxContext ForSelect()
        {
            Call<OqlSelectClause>();
            Call<OqlFromClause>();
            Call<OqlWhereClause>();
            Call<OqlOrderByClause>();
            Call<OqlNavigationClause>();

            return this;
        }

        protected virtual IOqlSyntaxContext ForUpdate()
        {
            Call<OqlUpdateClause>();
            Call<OqlWhereClause>();
            return this;
        }


        protected virtual IOqlSyntaxContext ForInsert()
        {
            Call<OqlInsertClause>();
            return this;
        }


        protected virtual IOqlSyntaxContext ForDelete()
        {
            Call<OqlDeleteClause>();
            Call<OqlFromClause>();
            Call<OqlWhereClause>();
            return this;
        }

        private OqlCallResult m_call_result = new OqlCallResult();

        public IOqlCallResult CallResult
        {
            get
            {
                return m_call_result;
            }
        }

    

        public abstract IQueryBuilder CreateQueryBuilder();

        public Expression ProcessMethodCall(IOqlExpressionVisitor caller, MethodCallExpression methodCall)
        {
            IOqlMethodCallEntry methodEntry = base[methodCall];

            if (methodEntry != null)
            {
                IOqlClause clause = methodEntry.Clause;

                if (clause != null)
                {
                    clause.ProcessMethodCall(this, methodCall);
                    return caller.Visit(methodCall.Arguments[0]);
                }

                IOqlMethodCallFormatter formatter = methodEntry.Formatter;

                if (formatter != null)
                {
                    formatter.FormatMethodCall(caller, methodCall);
                }
            }

            return methodCall;
        }

        public IOqlSyntaxContext InitializeFor(Expression expression)
        {

            if (expression.NodeType == ExpressionType.Call)
            {
                MethodCallExpression methodCall = expression as MethodCallExpression;

                if (methodCall.IsCalled(OqlInsertClause.InsertInfo))
                {
                    m_call_result.IsModifier = true;
                    return ForInsert();
                }

                if (methodCall.IsCalled(OqlUpdateClause.UpdateInfo))
                {
                    m_call_result.IsModifier = true;
                    return ForUpdate();
                }

                if (methodCall.IsCalled(OqlDeleteClause.DeleteInfo))
                {
                    m_call_result.IsModifier = true;
                    return ForDelete();
                }

            }

            m_call_result.IsModifier = false;
            return ForSelect();
        }
    }
}
