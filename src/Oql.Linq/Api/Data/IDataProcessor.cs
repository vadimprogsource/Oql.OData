using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataProcessor<T>
    {
        T ProcessInsert(T instance);

        T ProcessUpdate(IDataChangeSet<T> dataChangeSet);
        bool ProcessDelete(T instance);
    }
}
