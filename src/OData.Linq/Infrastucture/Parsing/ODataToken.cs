using OData.Linq.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Oql.Linq.Api.Metadata;

namespace OData.Linq.Infrastucture.Parsing
{
    public class ODataToken : IODataToken ,IExpression
    {

        private readonly string m_token;

        public ODataToken(string token)
        {
            m_token = token;
        }

        public bool Add
        {
            get
            {
                return m_token.ToLowerInvariant() == "add";
            }
        }

        public bool And
        {
            get
            {
                return m_token.ToLowerInvariant() == "and";
            }
        }

        public bool Asc
        {
            get
            {
                return m_token.TrimEnd().ToLowerInvariant().EndsWith("asc");
            }
        }

        public bool Desc
        {
            get
            {
                return m_token.TrimEnd().ToLowerInvariant().EndsWith("desc");
            }
        }

        public bool Div
        {
            get
            {
                return m_token.ToLowerInvariant() == "div";
            }
        }

        public bool Eq
        {
            get
            {
                return m_token.ToLowerInvariant() == "eq";
            }
        }

        public IExpression Expression
        {
            get;
            internal set;
        }

        public bool Ge
        {
            get
            {
                return m_token.ToLowerInvariant() == "ge";
            }
        }

        public bool Gt
        {
            get
            {
                return m_token.ToLowerInvariant() == "gt";
            }
        }

        public bool IsMethodCall
        {
            get
            {
                return false;
            }
        }

        public bool IsOf
        {
            get
            {
                return false;
            }
        }

        public bool Le
        {
            get
            {
                return m_token.ToLowerInvariant() == "le";
            }
        }

        public bool Lt
        {
            get
            {
                return m_token.ToLowerInvariant() == "lt";
            }
        }

        public bool Mod
        {
            get
            {
                return m_token.ToLowerInvariant() == "mod";
            }
        }

        public bool Mul
        {
            get
            {
                return m_token.ToLowerInvariant() == "mul";
            }
        }

        public string Name
        {
            get
            {
                return GetName();
            }
        }

        public bool Ne
        {
            get
            {
                return m_token.ToLowerInvariant() == "ne";
            }
        }

        public bool Negate
        {
            get
            {
                return m_token.ToLowerInvariant() == "-";
            }
        }

        public bool Not
        {
            get
            {
                return m_token.ToLowerInvariant() == "not";
            }
        }

        public bool Or
        {
            get
            {
                return m_token.ToLowerInvariant() == "or";
            }
        }

        public bool Sub
        {
            get
            {
                return m_token.ToLowerInvariant() == "sub";
            }
        }

        public object Value
        {
            get
            {
                return m_token;
            }
        }

        public bool Xor
        {
            get
            {
                return m_token.ToLowerInvariant() == "xor";
            }
        }

        
        public string GetName()
        {
            return m_token.Split(' ').FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
        }

    
    }
}
