using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public  interface IDataProvider
    {
        IOqlExpressionVisitor CreateExpressionVisitor();
        IMetaDataProvider  GetMetadata();
        IDataSource        GetDataSource();
    }

    public interface IDataProvider<T>
    {
        Task<IDataResult<T>> GetData(IDataIdentity<T> dataIndetity);
        Task<IDataPage<T>>   GetPage     (IDataFilter dataFilter);
    }
}
