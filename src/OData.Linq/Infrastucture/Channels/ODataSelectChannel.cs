using System.Linq;
using OData.Linq.Api;
using Oql.Linq;

namespace OData.Linq.Infrastucture.Channels
{
    public class ODataSelectChannel<T> : BaseODataChannel<T>
    {
        protected override IQueryable ProcessQuery(IQueryable<T> query, IODataReader odata)
        {
            return query.SelectFor(odata.ReadExpression(','));
        }
    }
}
