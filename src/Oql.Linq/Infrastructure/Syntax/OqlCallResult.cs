using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax
{
    public class OqlCallResult : IOqlCallResult
    {

        public int ElementIndex { get; set; } = -1;
       
        public Type ResultType { get; set; }

        public int Offset { get; set; } = 0;
        public int Size { get; set; } = 0;


        public OqlCommandToken Command { get; set; } = OqlCommandToken.Select;
       
    }
}
