using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Syntax
{
    public interface IOqlCallResult
    {
        Type ResultType { get; set; }

        int Offset { get; set; }
        int Size { get; set; }

        bool IsScalar { get; set; }

        bool HasSingle { get; set; }
        bool CanDefault { get; set; }
        bool HasFirst { get; set; }
        bool HasLast { get; set; }
        bool HasElementAt { get; }

        int ElementIndex { get; set; }

        bool IsAny { get; set; }
        bool IsAll { get; set; }

        bool IsModifier { get; }

    }
}
