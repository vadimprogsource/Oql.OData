using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlInsertClause : OqlBaseClause
    {

        internal static Method Insert = new Method<IQueryable<object>>(x => x.Insert(y => y));


        private MemberInitExpression m_init_expression;

        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
            m_init_expression = methodCall.GetArgument(1) as MemberInitExpression;
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {

            visitor.Query.AppendInsert().AppendType(visitor.SourceType).AppendBeginExpression();

            Expression[] vals = new Expression[m_init_expression.Bindings.Count];

            for (int i = 0; i < vals.Length; i++)
            {
                MemberAssignment ma = m_init_expression.Bindings[i] as MemberAssignment;

                vals[i] = ma.Expression;

                if (i > 0)
                {
                    visitor.Query.AppendExpressionSeparator();
                }

                visitor.Query.AppendMember(ma.Member);
            }

            visitor.Query.AppendEndExpression().AppendValues().AppendBeginExpression();

            visitor.Visit(vals.First());

            foreach(Expression x in vals.Skip(1))
            {
                visitor.Query.AppendExpressionSeparator();
                visitor.Visit(x);
            }

            visitor.Query.AppendEndExpression();
        }
    }
}
