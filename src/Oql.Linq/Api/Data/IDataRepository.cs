using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataRepository
    {
        IDataTransaction TransactionScope { get; }
        IQueryable<T> AsQueryable<T>();
        IDataRepository<T> AsRepository<T>();
    }

    public interface IDataRepository<T>
    {
        Task<bool> InsertNew (IDataChangeSet<T> changeSet);
        Task<int> UpdateWhere(IDataChangeSet<T> changeSet,Expression<Func<T,bool>> condition);
        Task<int> DeleteWhere(Expression<Func<T, bool>> condition);
    }
}
