using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Api
{
    public interface IDataProcessor<T>
    {
        T ProcessInsert(T data);
        T ProcessUpdate(IChangeSet<T> data);
        bool ProcessDelete(T data);
    }
}
