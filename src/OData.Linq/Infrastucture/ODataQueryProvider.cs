using OData.Linq.Api;
using Oql.Linq.Api.Query;
using System.Linq;

namespace OData.Linq.Infrastucture
{
    public class ODataQueryProvider : IODataQueryProvider
    {

        private readonly IObjectQueryProvider  m_query_provider ;
        private readonly IODataQueryParser     m_query_parser   ;
        private readonly IODataChannelFactory  m_channel_factory;


        public ODataQueryProvider(IObjectQueryProvider provider,IODataQueryParser parser , IODataChannelFactory factory)
        {
            m_query_provider  = provider;
            m_query_parser    = parser;
            m_channel_factory = factory;
        }


        public IQueryable CreateQuery(string entityName, string queryString)
        {

            IQueryable queryable = m_query_provider.CreateQuery(entityName);

            foreach (IODataReader odata in m_query_parser.Parse(queryString))
            {
                IODataChannel channel = m_channel_factory.CreateChannel(odata.Key, queryable.ElementType);

                if (channel == null)
                {
                    return null;
                }

                queryable = channel.ProcessQuery(queryable, odata);
            }

            return queryable;
        }
    }
}
