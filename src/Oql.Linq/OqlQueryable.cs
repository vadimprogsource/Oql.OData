using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq
{
    public static class OqlQueryable
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IExpression expression)
        {
            return new OqlOrderProcessor().ProcessOrderBy(query, expression);
        }

        public static IQueryable SelectFor<T>(this IQueryable<T> query, IExpression expression)
        {
            return new OqlSelectProcessor().ProcessSelect( query , expression);
        }

        public static IQueryable<T> FilterBy<T>(this IQueryable<T> query, IExpression expression)
        {
            return new OqlFilterProcessor().ProcessFilterBy(query , expression);
        }
    }
}
