using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlUpdateClause : OqlBaseClause
    {
        internal static Method UpdateInfo = new  Method<IQueryable<object>>(x => x.Update(y => y));


        private  MemberInitExpression m_member_init; 

        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
            m_member_init = methodCall.Arguments.OfType<MemberInitExpression>().ElementAt(1);
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {

            visitor.Query.AppendUpdate().AppendType(visitor.SourceType).AppendSet();

            MemberAssignment ma = m_member_init.Bindings.OfType<MemberAssignment>().First();

            visitor.Query.AppendMember(ma.Member).AppendAssign();
            visitor.Visit(ma.Expression);

            foreach (MemberAssignment x in m_member_init.Bindings.Skip(1))
            {
                visitor.Query.AppendExpressionSeparator().AppendMember(x.Member).AppendAssign();
                visitor.Visit(x.Expression);
            }

        }
    }
}
