using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataProcessor<T>
    {
        IDataResult<T> ProcessInsert(T instance);
        IDataResult<T> ProcessUpdate(IDataChangeSet<T> dataChangeSet);
        bool ProcessDelete(T instance);
    }
}
