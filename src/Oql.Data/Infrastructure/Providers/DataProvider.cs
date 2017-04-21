using Oql.Data.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Infrastructure.Providers
{
    public abstract class DataProvider<T> : IDataProvider<T>
    {
        public abstract Task<T> Lookup(ILookupOptions options);

        protected abstract IQueryable<T> CreateQuery();

        public abstract Task<IPageResult<T>> GetPage(IFilter filter);


    }
}
