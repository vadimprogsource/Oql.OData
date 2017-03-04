using Oql.Data.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Infrastructure.Providers
{
    public abstract class DataProvider<T> : IDataProvider<T>
    {
        public abstract T SelectSingle(object id);

        protected abstract IQueryable<T> CreateQuery();

        public IPageResult<T> SelectPage(IFilter filter)
        {
            throw new NotImplementedException();
        }

       
    }
}
