using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Query.Processors
{
    public class OqlOrderByCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
    {
        public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
        {
            return query.OrderBy(lambda);
        }
    }

    public class OqlThenByCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
    {
        public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
        {
            return (query as IOrderedQueryable<TEntity>).ThenBy(lambda);
        }
    }

    public class OqlOrderByDescendingCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
    {
        public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
        {
            return query.OrderByDescending(lambda);
        }
    }

    public class OqlThenByDescendingCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
    {
        public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
        {
            return (query as IOrderedQueryable<TEntity>).ThenByDescending(lambda);
        }
    }

}
