using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlOrderByClause : OqlStackClause
    {

        public static Method OrderByInfo           = new Method<IOrderedQueryable<object>>(x => x.OrderBy(y => y.GetHashCode()));
        public static Method ThenByInfo            = new Method<IOrderedQueryable<object>>(x => x.ThenBy (y => y.GetHashCode()));
        public static Method OrderByDescendingInfo = new Method<IOrderedQueryable<object>>(x => x.OrderByDescending(y => y.GetHashCode()));
        public static Method ThenByDescendingInfo  = new Method<IOrderedQueryable<object>>(x => x.ThenByDescending(y => y.GetHashCode()));


        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
            Push(methodCall.Method, methodCall.Arguments[1]);
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return OrderByInfo;
            yield return ThenByInfo;
            yield return OrderByDescendingInfo;
            yield return ThenByDescendingInfo;
       }


        protected virtual void VisitForAsc(IOqlExpressionVisitor visitor, Expression expression)
        {
            visitor.Visit(expression);
            visitor.Query.AppendAsc();
        }


        protected virtual void VisitForDesc(IOqlExpressionVisitor visitor, Expression expression)
        {
            visitor.Visit(expression);
            visitor.Query.AppendDesc();
        }


        protected override void PopVisit(IOqlExpressionVisitor visitor, MethodInfo method, Expression expression)
        {
            if (OrderByInfo.Equals(method) || ThenByInfo.Equals(method))
            {
                VisitForAsc(visitor, expression);
                return;
            }

            VisitForDesc(visitor, expression);
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            if (IsEmpty)
            {
                return;
            }

            visitor.Query.AppendOrderBy();

            base.VisitTo(visitor);
        }
    }
}
