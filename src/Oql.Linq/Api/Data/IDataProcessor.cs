using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataProcessor<T>
    {
        Task<IDataResult<T>> ProcessInsert(T instance);
        Task<IDataResult<T>> ProcessUpdate(IDataChangeSet<T> dataChangeSet);
        Task<bool> ProcessDelete(T instance);
    }
}
