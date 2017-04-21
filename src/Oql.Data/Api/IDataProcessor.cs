using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IDataProcessor<T>
    {
        Task<T> ProcessCreate(params object[] @params);
        Task<T> ProcessInsert(T data);
        Task<T> ProcessUpdate(IChangeSet<T> data);
        Task<bool> ProcessDelete(T data);
    }
}
