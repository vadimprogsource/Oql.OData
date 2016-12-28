using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Api
{
    public interface IODataQueryProvider
    {
        IQueryable CreateQuery(string entityName, string queryString);
    }
}
