using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlWhereClause : OqlBaseClause
    {

        private static Method Where     = new Method<IQueryable<object>>(x => x.Where(y => true));
        private static Method SkipWhile = new Method<IQueryable<object>>(x => x.SkipWhile(y => true));




        private Expression m_expression;


        protected Expression Expression { set { m_expression = value; } }

        protected void AndAlso(Expression expression)
        {
            if (m_expression == null)
            {
                m_expression = expression;
                return;
            }

            m_expression = Expression.AndAlso(m_expression, expression);
        }

        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {

            if (methodCall.IsCalled(SkipWhile))
            {
                AndAlso(Expression.Not(methodCall.GetArgument(1)));
                return;
            }

            AndAlso(methodCall.GetArgument(1));

            OqlNavigationClause.ProcessNavigate(callContext, methodCall);

        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            if (m_expression != null)
            {
                visitor.Query.AppendWhere();
                visitor.Visit(m_expression);
            }
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            return new[] { Where, SkipWhile }.Union(OqlNavigationClause.WithPredicates());
        }
    }
}
