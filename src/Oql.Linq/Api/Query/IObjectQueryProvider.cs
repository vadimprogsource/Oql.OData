using Oql.Linq.Api.CodeGen;
using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Query
{

    public interface IObjectQueryProvider : IQueryProvider
    {
        IQueryable          CreateQuery(string entityName);
        IQueryable          CreateQuery(IEntityInfo entityInfo);
        IQueryable<TEntity> CreateQuery<TEntity>();

        IExpressionBuilder CreateExpressionBuilder();

        IObjectQueryProcessor<TEntity> CreateQueryProcessor<TEntity>(IQueryable<TEntity> queryable);

    }
}
