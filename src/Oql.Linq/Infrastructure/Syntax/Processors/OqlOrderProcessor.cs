using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax.Processors
{

    public class OqlOrderProcessor
    {
        private interface ICommand
        {
            IQueryable ExecuteCommand(IQueryable query, LambdaExpression expression);
        }


        private  class OrderByCommand<TEntity,TProperty> : ICommand
        {
            public IQueryable ExecuteCommand(IQueryable query, LambdaExpression expression)
            {
                return ExecuteOrderBy(query as IQueryable<TEntity>, expression as Expression<Func<TEntity, TProperty>>);
            }

            public virtual IQueryable ExecuteOrderBy(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> expession)
            {
                return query.OrderBy(expession);
            }
      
        }

        private  class ThenByCommand<TEntity, TProperty> : ICommand
        {
            public IQueryable ExecuteCommand(IQueryable query, LambdaExpression expression)
            {
                return ExecuteThenBy(query as IOrderedQueryable<TEntity>, expression as Expression<Func<TEntity, TProperty>>);
            }

            public virtual IQueryable ExecuteThenBy(IOrderedQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> expession)
            {
                return query.ThenBy(expession);
            }

        }


        private class OrderByDescendingCommand<TEntity, TProperty> : OrderByCommand<TEntity, TProperty>
        {
            public override IQueryable ExecuteOrderBy(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> expession)
            {
                return query.OrderByDescending(expession);
            }
        }


        private class ThenByDescendingCommand<TEntity, TProperty> : ThenByCommand<TEntity, TProperty>
        {
            public override IQueryable ExecuteThenBy(IOrderedQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> expession)
            {
                return query.ThenByDescending(expession);
            }
        }

        


        private IQueryable ExecuteCommand<T>(IQueryable query , Type commandType , string propertyOrField)
        {
            ParameterExpression x   = Expression.Parameter(typeof(T), "x");
            LambdaExpression lambda = Expression.Lambda(Expression.PropertyOrField(x, propertyOrField), x);

            ICommand command = Activator.CreateInstance(commandType.MakeGenericType(typeof(T), lambda.Body.Type), true) as ICommand;

            return command.ExecuteCommand(query, lambda);
        }


        public IQueryable<T> ProcessOrderBy<T>(IQueryable<T> query, IExpression expression)
        {

            IQueryable q;


            if (expression.Desc)
            {
                q = ExecuteCommand<T>(query, typeof(OrderByDescendingCommand<,>), expression.Name);
            }
            else
            {
                q = ExecuteCommand<T>(query, typeof(OrderByCommand<,>), expression.Name);
            }


            for (IExpression x = expression.Expression; x != null; x = x.Expression)
            {
                if (x.Desc)
                {
                    q = ExecuteCommand<T>(q, typeof(ThenByDescendingCommand<,>), x.Name);
                    continue;
                }

                q = ExecuteCommand<T>(q, typeof(ThenByCommand<,>), x.Name);

            }

            return q as IQueryable<T>;
        }

    }
}
