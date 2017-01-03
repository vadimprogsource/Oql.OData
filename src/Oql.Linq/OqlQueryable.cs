using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Query;
using Oql.Linq.Infrastructure.Syntax.Clauses;
using System;
using System.Linq;
using System.Linq.Expressions;

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

    }
}
