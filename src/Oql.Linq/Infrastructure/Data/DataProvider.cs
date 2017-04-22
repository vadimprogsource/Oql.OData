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

        public abstract Task<IDataResult<T>> GetData(IDataIdentity<T> dataIndetity);
      
        public Task<IDataPage<T>> GetPage(IDataFilter dataFilter)
        {
            IQueryable<T> query = CreateQuery();
            return Task.FromResult( new DataPage<T>(dataFilter.PageSize, dataFilter.PageIndex, query) as IDataPage<T>);
        }

      
    }
}
