﻿using Oql.Linq.Infrastructure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlOrderByClause : OqlStackClause
    {

        public static Method OrderByInfo           = new Method<IOrderedQueryable<object>>(x => x.OrderBy(y => y.GetHashCode()));
        public static Method ThenByInfo            = new Method<IOrderedQueryable<object>>(x => x.ThenBy (y => y.GetHashCode()));
        public static Method OrderByDescendingInfo = new Method<IOrderedQueryable<object>>(x => x.OrderByDescending(y => y.GetHashCode()));
        public static Method ThenByDescendingInfo  = new Method<IOrderedQueryable<object>>(x => x.ThenByDescending(y => y.GetHashCode()));


        public override void AddMethodCall(MethodCallExpression methodCall)
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
            visitor.QueryBuilder.AppendAsc();
        }


        protected virtual void VisitForDesc(IOqlExpressionVisitor visitor, Expression expression)
        {
            visitor.Visit(expression);
            visitor.QueryBuilder.AppendDesc();
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

            visitor.QueryBuilder.AppendOrderBy();

            base.VisitTo(visitor);
        }
    }
}