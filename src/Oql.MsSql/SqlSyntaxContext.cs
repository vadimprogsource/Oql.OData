using Oql.Linq.Infrastructure.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oql.Linq.Api;

namespace Oql.MsSql
{
    public class SqlSyntaxContext : OqlSyntaxContext
    {
        public override IQueryBuilder CreateQueryBuilder()
        {
            return new SqlQueryBuilder();
        }
    }
}
