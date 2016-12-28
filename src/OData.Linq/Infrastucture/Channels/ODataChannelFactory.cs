using OData.Linq.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Infrastucture.Channels
{
    public class ODataChannelFactory : IODataChannelFactory
    {
        private readonly Dictionary<string, Type> m_channel_types = new Dictionary<string, Type>();


       public ODataChannelFactory Register(string key , Type genericType)
       {
            m_channel_types.Add(key.ToLowerInvariant(), genericType);
            return this;
       }


        public ODataChannelFactory()
        {
            Register("$top"    , typeof(ODataTopChannel<>   ));
            Register("$skip"   , typeof(ODataSkipChannel<>  ));
            Register("$select" , typeof(ODataSelectChannel<>));
            Register("$filter" , typeof(ODataFilterChannel<>));
            Register("$orderby", typeof(ODataOrderByChannel<>));
            Register("$extend" , typeof(ODataExtendChannel<>));
        }




        public IODataChannel CreateChannel(string key, Type queryType)
        {

            Type channelType;

            if (m_channel_types.TryGetValue(key, out channelType))
            {
                return Activator.CreateInstance(channelType.MakeGenericType(queryType)) as IODataChannel;
            }

            return null;
        }
    }
}
