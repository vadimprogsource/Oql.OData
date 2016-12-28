using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IMetadata
    {
        string Name { get; }
        Type  BaseType { get; }
    }
}
