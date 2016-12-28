using OData.Linq.Api;
using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Linq.Infrastucture.Parsing
{
    public class ODataReader : IODataReader
    {
        private string m_key ;
        private string m_data;


        public ODataReader(string key, string data)
        {
            m_key  = key;
            m_data = data;
        }


        public string Key
        {
            get
            {
                return m_key;
            }
        }

        public string ReadAll()
        {
            return m_data;
        }

        public int ReadInt()
        {
            return int.Parse(m_data);
        }

        public IEnumerable<IODataToken> ReadTokens(char separator)
        {
            return m_data.Split(separator).Select(x => new ODataToken(x));
        }

        public IExpression ReadExpression(char separator)
        {

            ODataToken head = null,tail = null;


            foreach (ODataToken token in m_data.Split(separator).Select(x => new ODataToken(x)))
            {
                if (head == null)
                {
                    head = tail = token;
                    continue;
                }

                tail.Expression = token;
                tail = token;

            }


            return head;
        }

        public string ReadQueryString()
        {
            return "?" + m_key + m_data;
        }
    }
}
