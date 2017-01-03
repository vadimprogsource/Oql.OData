using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Syntax.Methods;
using Oql.Linq.Api.Data;
using Oql.Linq.Infrastructure.Data;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlInsertClause : OqlBaseClause
    {

        internal static Method Insert = new Method<IQueryable<object>>(x => x.Insert(y => y));

        private IDataChangeSet m_change_set;


        protected IDataChangeSet ChangeSet { get { return m_change_set; } }

        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {

            Expression x = methodCall.GetArgument(1);

            if (x.NodeType == ExpressionType.MemberInit)
            {
                m_change_set = new MemberInitChangeSet(x as MemberInitExpression);
                return;
            }

            object obj = x.GetValue();

            if (obj is IDataChangeSet)
            {
                m_change_set = obj as IDataChangeSet;
                return;
            }


            m_change_set = new ObjectChangeSet(obj);

        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {

            visitor.Query.AppendInsert().AppendType(visitor.SourceType).AppendBeginExpression();

            Expression       val;
            List<Expression> vals = new List<Expression>();


            IDataChange dc = m_change_set.First();

            val = dc.NewValue;
            visitor.Query.AppendMember(dc.PropertyOrField);


            foreach (IDataChange x in m_change_set.Skip(1))
            {
                visitor.Query.AppendExpressionSeparator();
                visitor.Query.AppendMember(x.PropertyOrField);

            }

            visitor.Query.AppendEndExpression().AppendValues().AppendBeginExpression();

            visitor.Visit(val);

            foreach (Expression x in vals)
            {
                visitor.Query.AppendExpressionSeparator();
                visitor.Visit(x);
            }

            visitor.Query.AppendEndExpression();
        }
    }
}
