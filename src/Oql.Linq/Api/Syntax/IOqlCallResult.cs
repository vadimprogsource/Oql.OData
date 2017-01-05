using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{


    public enum OqlCommandToken : byte {Select = 0 ,Single =1 , First ,ElementAt , Last , Exec , Scalar ,IsAll , IsAny , DefaultFlag =   128    } 


    public interface IOqlCallResult
    {
        Type ResultType { get; set; }

        int Offset { get; set; }
        int Size { get; set; }

        int ElementIndex { get; set; }

        OqlCommandToken Command { get; set; }

    }
}
