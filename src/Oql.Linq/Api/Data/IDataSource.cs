using System;

namespace Oql.Linq.Api.Data
{
    public interface IDataSource : IDisposable
    {

        IDataSet GetDataSet(IQueryBuilder query);
        object GetScalar(IQueryBuilder query);
        int ExecuteCommand(IQueryBuilder query);
    }
}
