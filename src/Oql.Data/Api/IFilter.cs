using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IFilter
    {

        int PageIndex { get; }
        int PageSize { get; }
        bool HasFiltered { get; }
        bool HasOrdered { get; }

        IEnumerable<IFilterCriteria> Where { get; }
        IEnumerable<IOrderCriteria> OrderBy { get; }


    }
}
