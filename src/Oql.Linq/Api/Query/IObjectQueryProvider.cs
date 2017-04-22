
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
        IQueryable          CreateQuery(IEntity entityInfo);
        IQueryable<TEntity> CreateQuery<TEntity>();

        CodeGen.IExpressionBuilder CreateExpressionBuilder();

        IObjectQueryProcessor<TEntity> CreateQueryProcessor<TEntity>(IQueryable<TEntity> queryable);

        Task<TResult> ExecuteAsync<TResult>(Expression expression);

        Task<object> ExecuteAsync(Expression expression);
    }
}
