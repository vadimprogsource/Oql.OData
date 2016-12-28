using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Query;
using Oql.Linq.Api.CodeGen;

namespace Oql.Linq.Infrastructure.Syntax.Processors
{
    public class OqlSelectProcessor : OqlBaseProcessor
    {

        private class SelectCommand<TEntity, TResult> : OqlBaseCommand<TEntity, TResult>
        {
            public override IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> lambda)
            {
                return query.Select(lambda);
            }
        }

        public IQueryable ProcessSelect<T>(IQueryable<T> query, IExpression expression)
        {
            IExpressionBuilder expressionBulider = (query.Provider as IObjectQueryProvider).CreateExpressionBuilder();
            return ExecuteCommand(query , typeof(SelectCommand<,>) , expressionBulider.MakeMembersInit(query.ElementType , expression.GetIterator().Select(x=>x.Name).ToArray()));
        }
    }
}
