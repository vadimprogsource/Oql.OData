using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Api
{
    public interface IODataChannel
    {
        IQueryable ProcessQuery(IQueryable query, IODataReader odata);
    }
}
