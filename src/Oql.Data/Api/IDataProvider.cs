using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IDataProvider<T>
    {
        T SelectSingle(object id);
        IPageResult<T> SelectPage(IFilter filter);
    }
}
