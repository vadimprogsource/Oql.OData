using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IMetadataProvider
    {
        IEntityInfo GetEntity(string typeName, params IMemberInfo[] members);
    }
}
