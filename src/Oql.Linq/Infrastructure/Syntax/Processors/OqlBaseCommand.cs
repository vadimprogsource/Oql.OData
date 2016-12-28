using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax.Processors
{


    public interface IOqlCommand
    {
        IQueryable ExecuteCommand(IQueryable query, LambdaExpression expression);
    }

    public abstract class OqlBaseCommand<TEntity, TProperty> : IOqlCommand
    {
        IQueryable IOqlCommand.ExecuteCommand(IQueryable query, LambdaExpression lambda)
        {
            return ExecuteCommand(query as IQueryable<TEntity>, lambda as Expression<Func<TEntity, TProperty>>);
        }

        public abstract IQueryable ExecuteCommand(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> lambda);
    }


    public class OqlBaseProcessor
    {
        protected IQueryable ExecuteCommand(IQueryable query,Type commandType ,  LambdaExpression lambda)
        {
           return  (Activator.CreateInstance(commandType.MakeGenericType(query.ElementType, lambda.Body.Type)) as IOqlCommand).ExecuteCommand(query, lambda);
        }
    }
}
