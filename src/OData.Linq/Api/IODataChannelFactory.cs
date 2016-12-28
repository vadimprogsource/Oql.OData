using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Api
{
    public interface IODataChannelFactory
    {
        IODataChannel CreateChannel(string key,Type queryType);
    }
}
