using Oql.Linq.Api.Metadata;
using System;

namespace Oql.Linq.Api.CodeGen
{
    public interface ICodeProvider
    {
        Type GetType(IEntity entity);
        Type GetType(IMetaDataProvider provider , string  entityName);

    }
}
