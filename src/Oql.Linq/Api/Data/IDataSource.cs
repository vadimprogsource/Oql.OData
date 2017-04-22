using System;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataSource : IDisposable
    {

        IDataSet GetDataSet(IQueryBuilder query);
        object GetScalar(IQueryBuilder query);
        int ExecuteCommand(IQueryBuilder query);
        Task<IDataSet> GetDataSetAsync(IQueryBuilder query);
        Task<object> GetScalarAsync(IQueryBuilder query);
        Task<int> ExecuteCommandAsync(IQueryBuilder query);
    }
}
