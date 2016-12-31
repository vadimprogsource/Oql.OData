using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Query.Processors
{

    public class OqlBaseProcessor 
    {
        protected static IQueryable ExecuteCommand(IQueryable query, Type commandType, LambdaExpression lambda)
        {
            return (Activator.CreateInstance(commandType.MakeGenericType(query.ElementType, lambda.Body.Type)) as IOqlCommand).ExecuteCommand(query, lambda);
        }
    }
}
