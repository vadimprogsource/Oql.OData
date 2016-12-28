using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IEntityInfo : IMetadata 
    {
        Type RuntimeType                   { get; }
        IMemberInfo[] Members   { get; }
        IMemberInfo this[Expression propertyOrField] { get; }
    }
}
