using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlOrderByClause : OqlBaseClause
    {

        public static Method OrderBy           = new Method<IOrderedQueryable<object>>(x => x.OrderBy(y => y.GetHashCode()));
        public static Method ThenBy            = new Method<IOrderedQueryable<object>>(x => x.ThenBy (y => y.GetHashCode()));
        public static Method OrderByDescending = new Method<IOrderedQueryable<object>>(x => x.OrderByDescending(y => y.GetHashCode()));
        public static Method ThenByDescending  = new Method<IOrderedQueryable<object>>(x => x.ThenByDescending(y => y.GetHashCode()));


        private Stack<MethodCallExpression> m_order_stack = new Stack<MethodCallExpression>();


        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
            m_order_stack.Push(methodCall);
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return OrderBy;
            yield return ThenBy;
            yield return OrderByDescending;
            yield return ThenByDescending;
       }


        protected virtual void VisitOrderBy(IOqlExpressionVisitor visitor,MethodCallExpression methodCall)
        {
            visitor.Visit(methodCall.GetArgument(1));


            if (methodCall.IsCalledOr(OrderBy, ThenBy))
            {

                visitor.Query.AppendAsc();
                return;
            }


            if (methodCall.IsCalledOr(OrderByDescending, ThenByDescending))
            {
                visitor.Query.AppendDesc();
            }


        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {

            if (m_order_stack.Count > 0)
            {
                visitor.Query.AppendOrderBy();

                VisitOrderBy(visitor,m_order_stack.Pop());

                while (m_order_stack.Count > 0)
                {
                    visitor.Query.AppendExpressionSeparator();
                    VisitOrderBy(visitor, m_order_stack.Pop());
                }
            }
        }
    }
}
