using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IMetaDataProvider
    {
        IEntity GetEntity(string typeName, bool lowerCase = true);

        IEntityBuilder<T> CreateEntityBuilder<T>();
        IEntityBuilder CreateEntityBuilder(Type baseType);

    }
}
