using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{
    public interface IEntity : IMetaData 
    {
        IFrom From { get; }
        Type RuntimeType                   { get; }
        IProperty[] Properties   { get; }
        IProperty this[Expression propertyOrField] { get; }
    }
}
