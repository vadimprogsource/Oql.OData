using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Api.Data
{
    public  interface IDataProvider
    {
        
        IOqlSyntaxProvider GetSyntax();
        IMetadataProvider  GetMetadata();
        IQueryBuilder      GetQuery();

        IDataSource        GetDataSource();
    }
}
