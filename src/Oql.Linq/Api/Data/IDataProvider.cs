using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax;
using System;
using System.Linq.Expressions;

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
        IDataResult<T> SingleOrDefault(Guid dataIndetity);
        IDataPage<T>   SelectPage     (IDataFilter dataFilter);
    }
}
