using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OData.Linq.Api;
using System.Linq.Expressions;
using Oql.Linq;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Query;

namespace OData.Linq.Infrastucture.Channels
{
    public class ODataOrderByChannel<T> : BaseODataChannel<T>
    {
        
        protected override IQueryable ProcessQuery(IQueryable<T> query, IODataReader odata)
        {
            return query.OrderBy(odata.ReadExpression(','));
        }
    }
}
