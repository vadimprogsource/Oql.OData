using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Api
{
    public interface IODataReader
    {
        string Key { get; }
        IEnumerable<IODataToken> ReadTokens(char separator);
        IExpression ReadExpression(char separator);

        string ReadQueryString();

        string ReadAll(); 

        int ReadInt();  

    }
}
