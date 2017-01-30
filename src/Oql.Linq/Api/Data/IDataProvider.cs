using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Api.Data
{
    public  interface IDataProvider
    {
        IOqlExpressionVisitor CreateExpressionVisitor();
        IMetaDataProvider  GetMetadata();
        IDataSource        GetDataSource();
    }
}
