using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlFromClause : OqlBaseClause 
    {


        public static readonly IMethod OuterJoin = new Method<IQueryable<object>>(x => x.OuterJoin(y => int.MaxValue,(y, z) => true));
        public static readonly IMethod InnerJoin = new Method<IQueryable<object>>(x => x.InnerJoin(y => int.MaxValue,(y, z) => true));


        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
            if (methodCall.IsCalled(OuterJoin))
            {
            }

            if (methodCall.IsCalled(InnerJoin))
            {
            }
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            visitor.Query.AppendFrom().AppendType(visitor.SourceType);
        }

        public override IEnumerable<IMethod> GetMethods()
        {
            return new[] { OuterJoin , InnerJoin};
        }
    }
}
