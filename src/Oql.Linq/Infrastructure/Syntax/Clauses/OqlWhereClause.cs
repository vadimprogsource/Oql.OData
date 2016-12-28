using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Oql.Linq.Api;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlWhereClause : OqlBaseClause
    {
        private Expression m_expression;


        protected Expression Expression { set { m_expression = value; } }

        protected void AndAlso(Expression expression)
        {
            if (m_expression == null)
            {
                m_expression = expression.GetInnerExpression();
                return;
            }

            m_expression = Expression.AndAlso(m_expression, expression.GetInnerExpression());
        }

        public override void AddMethodCall(MethodCallExpression methodCall)
        {
            AndAlso(methodCall.Arguments[1]);
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            if (m_expression != null)
            {
                visitor.QueryBuilder.AppendWhere();
                visitor.Visit(m_expression);
            }
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return new Method<IQueryable<object>>(x => x.Where(y => true));
        }
    }
}
