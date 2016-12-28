using OData.Linq.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Infrastucture.Parsing
{
    public class ODataQueryParser : IODataQueryParser
    {
        public IEnumerable<IODataReader> Parse(string queryString)
        {
            string[] queryParts = queryString.Split('$', '=','?').Where(x=>!string.IsNullOrWhiteSpace(x)).ToArray();

            for (int i = 0; i < queryParts.Length; i++)
            {
                yield return new ODataReader("$" + queryParts[i], queryParts[++i]);
            }
        }
    }
}
