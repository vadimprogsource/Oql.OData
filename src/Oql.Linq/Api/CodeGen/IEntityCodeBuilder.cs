using Oql.Linq.Api.Metadata;
using System;

namespace Oql.Linq.Api.CodeGen
{
    public interface IEntityCodeBuilder
    {
        Type Build(IEntityInfo entityInfo);
    }
}
