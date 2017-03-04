using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{

    public interface IDataFilterCriteria
    {
        string PropertyOrField { get; }
        object Value           { get; }
    }

    public interface IDataOrderCriteria
    {
        string PropertyOrField { get; }
        bool Asc { get; }
    }

    public interface IDataFilter
    {
         int PageSize  { get; }
         int PageIndex { get; }

         bool HasFiltered { get; }
         bool HasOrdered  { get; }

         IEnumerable<IDataFilterCriteria> Where   { get; }
         IEnumerable<IDataOrderCriteria>  OrderBy { get; }

    }
}
