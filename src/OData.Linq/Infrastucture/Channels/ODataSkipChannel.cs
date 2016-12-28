using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OData.Linq.Api;

namespace OData.Linq.Infrastucture.Channels
{
    public class ODataSkipChannel<T> : BaseODataChannel<T>
    {
        protected override IQueryable ProcessQuery(IQueryable<T> query, IODataReader odata)
        {
            return query.Skip(odata.ReadInt());
        }
    }
}
