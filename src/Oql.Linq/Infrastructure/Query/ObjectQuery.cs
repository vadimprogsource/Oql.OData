using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Query
{


    public class ObjectQuery<TEntity> : IQueryable<TEntity>, IOrderedQueryable<TEntity>
    {

        private readonly IQueryProvider m_provider;
        private readonly Expression     m_expression;

        public Type ElementType
        {
            get
            {
                return typeof(TEntity);
            }
        }

        public Expression Expression
        {
            get
            {
                return m_expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return m_provider;
            }
        }


        public ObjectQuery(IQueryProvider provider)
        {
            m_provider   = provider;
            m_expression = Expression.Constant(this);
        }

        public ObjectQuery(IQueryProvider provider, Expression expression)
        {
            m_provider = provider;
            m_expression = expression;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return m_provider.Execute<IEnumerable<TEntity>>(m_expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    
}
