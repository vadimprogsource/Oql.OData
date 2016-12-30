using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Infrastructure.Syntax.Clauses;
using Oql.Linq.Infrastructure.Syntax.Methods;
using System.Collections.Generic;
using Oql.Linq.Api;
using Oql.Linq.Infrastructure.Syntax.Formatters;

namespace Oql.Linq.Infrastructure.Syntax
{
    public abstract class OqlSyntaxContext : MethodSet ,  IOqlSyntaxContext
    {

        public IOqlBitwiseFormatter BitwiseFormatter { get; protected set; } = new OqlBitwiseFormatter();

        public IOqlBooleanFormatter BooleanFormatter { get; protected set; } = new OqlBooleanFormatter();

        public IOqlComparisonFormatter ComparisonFormatter { get; protected set; } = new OqlComparisonFormatter();
        public IOqlMathFormatter MathFormatter { get; protected set; } = new OqlMathFormatter();

        public IOqlTakeByClause Taken { get;  set; }


        public OqlSyntaxContext()
        {
            Call<OqlEndsWithFormatter>  ();
            Call<OqlStartsWithFormatter>();
            Call<OqlContainsFormatter>  ();
        }

        public IEnumerable<IOqlClause> Clauses
        {
            get
            {
                return this;
            }
        }


        public virtual OqlSyntaxContext ForSelect()
        {
            Call<OqlSelectClause>();
            Call<OqlFromClause>();
            Call<OqlWhereClause>();
            Call<OqlOrderByClause>();
            Taken = Call<OqlTakeByClause>();

            return this;
        }

        public virtual OqlSyntaxContext ForUpdate()
        {
            Call<OqlUpdateClause>();
            Call<OqlWhereClause>();
            return this;
        }


        public virtual OqlSyntaxContext ForInsert()
        {
            Call<OqlInsertClause>();
            return this;
        }


        public virtual OqlSyntaxContext ForDelete()
        {
            Call<OqlDeleteClause>();
            Call<OqlFromClause>();
            Call<OqlWhereClause>();
            return this;
        }

        public abstract IQueryBuilder CreateQueryBuilder();
       
    }
}
