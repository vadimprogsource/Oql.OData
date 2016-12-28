using Oql.Linq.Api.Metadata;
using System;

namespace Oql.Linq.Api.CodeGen
{
    public interface ICodeProvider
    {
        Type GetType(IEntityInfo entity);
        Type GetType(IMetadataProvider provider , string  entityName);

    }
}
