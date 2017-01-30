using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{

    public interface IRelation
    {
        bool IsOuterJoin { get; }
        IEntity To { get; }
        object  On { get; }
        object Body { get; }
    }
}
