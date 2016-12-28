using System;
using System.Linq;
using OData.Linq.Api;
using Oql.Linq;


namespace OData.Linq.Infrastucture.Channels
{
    public class ODataFilterChannel<T> : BaseODataChannel<T>
    {
        protected override IQueryable ProcessQuery(IQueryable<T> query, IODataReader odata)
        {
            return query.FilterBy(odata.ReadExpression(' '));
        }
    }
}
