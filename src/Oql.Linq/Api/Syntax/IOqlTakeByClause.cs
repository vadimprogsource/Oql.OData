using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlTakeByClause : IOqlClause
    {
        int Offset { get; }
        int Size  { get; }
    }

}
