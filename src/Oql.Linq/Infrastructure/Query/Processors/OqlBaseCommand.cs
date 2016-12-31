using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Query.Processors
{


    public interface IOqlCommand
    {
        IQueryable ExecuteCommand(IQueryable query, LambdaExpression expression);
    }

    public abstract class OqlBaseCommand<TEntity, TResult> : IOqlCommand
    {
        IQueryable IOqlCommand.ExecuteCommand(IQueryable query, LambdaExpression lambda)
        {
            return ExecuteCommand(query as IQueryable<TEntity>, lambda as Expression<Func<TEntity, TResult>>);
        }

        public abstract IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda);
    }


}
