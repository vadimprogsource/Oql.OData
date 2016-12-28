using Oql.Linq.Api.CodeGen;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax.Processors
{

    public class OqlOrderProcessor : OqlBaseProcessor
    {
        private class OrderByCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
        {
            public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
            {
                return query.OrderBy(lambda);
            }
        }

        private class ThenByCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
        {
            public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
            {
                return (query as IOrderedQueryable<TEntity>).ThenBy(lambda);
            }
        }

        private class OrderByDescendingCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
        {
            public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
            {
                return query.OrderByDescending(lambda);
            }
        }

        private class ThenByDescendingCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
        {
            public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
            {
                return (query as IOrderedQueryable<TEntity>).ThenByDescending(lambda);
            }
        }






        public IQueryable<T> ProcessOrderBy<T>(IQueryable<T> query, IExpression expression)
        {

            IQueryable         q;
            IExpressionBuilder b = (query.Provider as IObjectQueryProvider).CreateExpressionBuilder();


            if (expression.Desc)
            {
                q = ExecuteCommand(query, typeof(OrderByDescendingCommand<,>), b.MakeMemberAccess(typeof(T), expression.Name));
            }
            else
            {
                q = ExecuteCommand(query, typeof(OrderByCommand<,>), b.MakeMemberAccess(typeof(T), expression.Name));
            }


            for (IExpression x = expression.Expression; x != null; x = x.Expression)
            {
                if (x.Desc)
                {
                    q = ExecuteCommand(q, typeof(ThenByDescendingCommand<,>), b.MakeMemberAccess(typeof(T), x.Name));
                    continue;
                }

                q = ExecuteCommand(q, typeof(ThenByCommand<,>), b.MakeMemberAccess(typeof(T), x.Name));

            }

            return q as IQueryable<T>;
        }

    }
}
