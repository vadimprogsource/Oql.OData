using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OData.Linq.Api;

namespace OData.Linq.Infrastucture.Channels
{
    public class ODataTopChannel<T> : BaseODataChannel<T>
    {
        protected override IQueryable ProcessQuery(IQueryable<T> query, IODataReader odata)
        {
            return query.Take(odata.ReadInt());
        }
    }
}
