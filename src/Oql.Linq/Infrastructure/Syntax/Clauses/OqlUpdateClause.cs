﻿using System.Linq;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Syntax.Methods;
using Oql.Linq.Api.Data;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlUpdateClause : OqlInsertClause
    {
        internal static IMethod Update = new  Method<IQueryable<object>>(x => x.Update(y => y));


        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            visitor.Query.AppendUpdate().AppendType(visitor.SourceType).AppendSet();

            IDataChange dc = ChangeSet.First();

            visitor.Query.AppendMember(dc.PropertyOrField).AppendAssign();
            visitor.Visit(dc.NewValue);

            foreach (IDataChange x in ChangeSet.Skip(1))
            {
                visitor.Query.AppendExpressionSeparator().AppendMember(dc.PropertyOrField).AppendAssign();
                visitor.Visit(dc.NewValue);
            }
        }
    }
}
