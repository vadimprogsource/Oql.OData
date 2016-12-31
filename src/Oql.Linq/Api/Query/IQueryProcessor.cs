using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Query
{
    public interface IObjectQueryProcessor<T>
    {
        IQueryable    ProcessSelect(IExpression expression);
        IQueryable<T> ProcessWhere(IExpression expression);
        IQueryable<T> ProcessOrderBy(IExpression expression);
    }
}
