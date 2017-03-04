using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{



    public interface IDataChange
    {
        MemberInfo PropertyOrField { get; }
        Expression NewValue        { get; }
    }

    public interface IDataChangeSet : IEnumerable<IDataChange> 
    {
        object Instance { get; }
    }

    public interface IDataChangeSet<T> : IDataChangeSet
    {
        bool IsModifier<V>(Expression<Func<T, V>> propertyOrField);
       new T Instance { get; } 
    }
}
