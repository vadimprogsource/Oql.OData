using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oql.Linq.Api;
using System.Reflection;
using Oql.Linq.Api.Syntax;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public abstract class OqlStackClause : OqlBaseClause
    {

        private Stack<Tuple<MethodInfo, Expression>> m_stack = new Stack<Tuple<MethodInfo, Expression>>();


        protected bool IsEmpty { get { return m_stack.Count < 1; } }

        protected void Push(MethodInfo method, Expression expression)
        {
            m_stack.Push(Tuple.Create(method, expression));
        }


        protected virtual void PopVisit(IOqlExpressionVisitor visitor , MethodInfo method , Expression expression)
        {
            visitor.Visit(expression);
        }


        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            if (m_stack.Count > 0)
            {
               
                Tuple<MethodInfo, Expression> x = m_stack.Pop();

                PopVisit(visitor, x.Item1, x.Item2);

                while (m_stack.Count > 0)
                {
                    visitor.QueryBuilder.AppendExpressionSeparator();
                    x = m_stack.Pop();
                    PopVisit(visitor, x.Item1, x.Item2);
                }

            }

        }
    }
}
