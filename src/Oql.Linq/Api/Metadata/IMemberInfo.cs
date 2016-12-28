using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IMemberInfo : IMetadata
    {
        bool AllowNull { get; }
        int Size { get; }
        bool IsPrimaryKey { get; }
        IEntityInfo RelatedEntity { get; }
    }
}
