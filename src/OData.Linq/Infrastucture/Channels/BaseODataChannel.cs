using OData.Linq.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Infrastucture.Channels
{
    public abstract class BaseODataChannel<T> : IODataChannel
    {
        public IQueryable ProcessQuery(IQueryable query, IODataReader odata)
        {
            return ProcessQuery(query as IQueryable<T>, odata);
        }

        protected abstract IQueryable ProcessQuery(IQueryable<T> query, IODataReader odata);
    }
}
