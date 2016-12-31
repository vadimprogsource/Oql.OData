using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Query.Processors
{

    public class OqlSelectCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
    {
        public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
        {
            return query.Select(lambda);
        }
    }
}
