using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax
{
    public class OqlCallResult : IOqlCallResult
    {
        public bool CanDefault { get; set; }

        public int ElementIndex { get; set; } = -1;
       
        public bool HasElementAt
        {
            get
            {
                return ElementIndex > -1;
            }
        }

        public bool HasFirst { get; set; } = false;

        public bool HasLast { get; set; } = false;

        public bool HasSingle { get; set; } = false;

        public bool IsAll { get; set; } = false;

        public bool IsAny { get; set; } = false;

        public bool IsScalar { get; set; } = false;

        public Type ResultType { get; set; }

        public int Offset { get; set; } = 0;
        public int Size { get; set; } = 0;

        public bool IsModifier { get; internal set; } = false;
        
    }
}
