using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IProperty : IMetaData
    {
        IEntity Entity { get; }
        bool AllowNull { get; }
        int Size { get; }
        bool IsPrimaryKey { get; }
        IRelation Join { get; }
        IScalarExpression Expression { get; }
    }
}
