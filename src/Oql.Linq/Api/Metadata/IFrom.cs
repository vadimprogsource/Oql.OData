using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IFrom
    {
        string UniqueKey { get; }
        string ForSelect { get; }
        string ForInsert { get; }
        string ForUpdate { get; }
        string ForDelete { get; }
        
    }
}
