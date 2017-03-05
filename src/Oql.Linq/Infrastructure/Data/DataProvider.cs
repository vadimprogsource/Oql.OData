using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Data
{
    public abstract class DataProvider<T> : IDataProvider<T>
    {

        protected abstract IQueryable<T> CreateQuery();

        public abstract IDataResult<T> SingleOrDefault(Guid dataIdentity);
      
        public IDataPage<T> SelectPage(IDataFilter dataFilter)
        {
            IQueryable<T> query = CreateQuery();
            return new DataPage<T>(dataFilter.PageSize, dataFilter.PageIndex, query);
        }

    }
}
