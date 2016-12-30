using Oql.Linq.Infrastructure.Syntax.Clauses;
using Oql.Linq.Infrastructure.Syntax.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Api.Data;
using Oql.Linq.Api.Metadata;
using System.Data.SqlClient;
using Oql.Linq.Api;

namespace Oql.MsSql
{
    public class SqlDataProvider : IDataProvider 
    {

        public IMetadataProvider GetMetadata()
        {
            return new SqlMetadataProvider(m_connection_string);
        }

        public IDataSource GetDataSource()
        {
            return new SqlDataSource(new SqlConnection(m_connection_string));
        }

        public IOqlExpressionVisitor CreateExpressionVisitor()
        {
            return new OqlExpressionVisitor(new SqlSyntaxContext().ForSelect());
        }

        private string m_connection_string;

        public SqlDataProvider(string connectionString)
        {
            m_connection_string = connectionString;
        }

    }
}
