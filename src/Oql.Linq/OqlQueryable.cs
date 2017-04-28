using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Query;
using Oql.Linq.Infrastructure.Syntax.Clauses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq
{
    public static class OqlQueryable
    {

        private static IObjectQueryProcessor<T> Processor<T>(this IQueryable<T> query)
        {
            return (query.Provider as IObjectQueryProvider).CreateQueryProcessor(query);
        }


        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IExpression expression)
        {
            return query.Processor().ProcessOrderBy(expression);
        }

        public static IQueryable SelectFor<T>(this IQueryable<T> query, IExpression expression)
        {
            return query.Processor().ProcessSelect(expression);
        }

        public static IQueryable<T> FilterBy<T>(this IQueryable<T> query, IExpression expression)
        {
            return  query.Processor().ProcessWhere(expression);
        }


        public static int Insert<TEntity,TResult>(this IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> expression)
        {
            return query.Provider.Execute<int>(OqlInsertClause.Insert.Call<TEntity, TResult>(query.Expression, expression));
        }

        public static int Update<TEntity,TResult>(this IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> expression)
        {
            return query.Provider.Execute<int>(OqlUpdateClause.Update.Call<TEntity, TResult>(query.Expression, expression));
        }

        public static int Delete<TEntity>(this IQueryable<TEntity> query)
        {
            return query.Provider.Execute<int>(OqlDeleteClause.Delete.Call<TEntity>(query.Expression));
        }

        public static IQueryable<TEntity> OuterJoin<TEntity, TRelation>(this IQueryable<TEntity> query, Expression<Func<TEntity, TRelation>> relation, Expression<Func<TEntity, TRelation, bool>> condition)
        {
          return query.Provider.CreateQuery<TEntity>(OqlFromClause.OuterJoin.Call<TEntity, TRelation>(query.Expression, relation, condition));
        }

        public static IQueryable<TEntity> InnerJoin<TEntity, TRelation>(this IQueryable<TEntity> query, Expression<Func<TEntity, TRelation>> relation, Expression<Func<TEntity, TRelation, bool>> condition)
        {
            return query.Provider.CreateQuery<TEntity>(OqlFromClause.InnerJoin.Call<TEntity, TRelation>(query.Expression, relation, condition));
        }

        public static Task<IEnumerable<T>> AsEnumerableAsync<T>(this IQueryable<T> query)
        {
            IObjectQueryProvider provider = query.Provider as IObjectQueryProvider;

            if (provider == null)
            {
                return Task.FromResult(query.Provider.Execute<IEnumerable<T>>(query.Expression));
            }

            return provider.ExecuteAsync<IEnumerable<T>>(query.Expression); 
        }


        public static Task<IEnumerable> AsEnumerableAsync(this IQueryable query)
        {
            IObjectQueryProvider provider = query.Provider as IObjectQueryProvider;

            if (provider == null)
            {
                return Task.FromResult(query.Provider.Execute(query.Expression) as IEnumerable);
            }

            return provider.ExecuteAsync(query.Expression).ContinueWith(x=>x.Result as IEnumerable);
        }

    }
}
