using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataPage<T>
    {
         int PageSize { get;  }
         int PageIndex { get;  }
         int Pages { get; }
         int TotalRecs { get; }

         IEnumerable<T> Page { get; }
    }
}
