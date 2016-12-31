using Oql.Linq.Api.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oql.Linq.Api.Metadata;
using System.Linq.Expressions;
using Oql.Linq.Api.CodeGen;

namespace Oql.Linq.Infrastructure.Query.Processors
{
    public class ObjectQueryProcessor<T> : OqlBaseProcessor, IObjectQueryProcessor<T>
    {

        private readonly IQueryable<T> m_query;

        public ObjectQueryProcessor(IQueryable<T> queryable)
        {
            m_query = queryable;
        }

        public IQueryable ProcessSelect(IExpression expression)
        {
            IExpressionBuilder expressionBulider = (m_query.Provider as IObjectQueryProvider).CreateExpressionBuilder();
            return ExecuteCommand(m_query, typeof(OqlSelectCommand<,>), expressionBulider.MakeMembersInit(m_query.ElementType, expression.GetIterator().Select(x => x.Name).ToArray()));
        }

        public IQueryable<T> ProcessWhere(IExpression expression)
        {
            return m_query.Where(new OqlFilterProcessor().ProcessFilter<T>(expression));
        }

        public IQueryable<T> ProcessOrderBy(IExpression expression)
        {
            IQueryable q;
            IExpressionBuilder b = (m_query.Provider as IObjectQueryProvider).CreateExpressionBuilder();


            if (expression.Desc)
            {
                q = ExecuteCommand(m_query, typeof(OqlOrderByDescendingCommand<,>), b.MakeMemberAccess(typeof(T), expression.Name));
            }
            else
            {
                q = ExecuteCommand(m_query, typeof(OqlOrderByCommand<,>), b.MakeMemberAccess(typeof(T), expression.Name));
            }


            for (IExpression x = expression.Expression; x != null; x = x.Expression)
            {
                if (x.Desc)
                {
                    q = ExecuteCommand(q, typeof(OqlThenByDescendingCommand<,>), b.MakeMemberAccess(typeof(T), x.Name));
                    continue;
                }

                q = ExecuteCommand(q, typeof(OqlThenByCommand<,>), b.MakeMemberAccess(typeof(T), x.Name));

            }

            return q as IQueryable<T>;
        }

        
    }
}
