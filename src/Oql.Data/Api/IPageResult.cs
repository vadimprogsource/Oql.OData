using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IPageResult<T>
    {
        int PageSize { get; }
        int PageIndex { get; }
        int Pages { get; }
        int Total { get; }

        IEnumerable<T> Page { get; }
    }
}
