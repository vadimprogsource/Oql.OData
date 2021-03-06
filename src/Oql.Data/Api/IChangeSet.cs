﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IChangeSet<T>
    {
        bool IsModified<V>(Expression<Func<T, V>> propertyOrField);
        void  SetModified<V>(Expression<Func<T, V>> propertyOrField);
        Expression ChangedMembers { get; }
        T Instance { get; }
        long Log { get; }
    }
}
