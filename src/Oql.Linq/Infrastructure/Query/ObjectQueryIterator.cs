using Oql.Linq.Api;
using Oql.Linq.Api.Data;
using Oql.Linq.Api.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Query
{
    public class ObjectQueryIterator<TEntity> : IEnumerable<TEntity> , IEnumerator<TEntity>, IDisposable
    {

        private readonly IDataSource       m_data_source;
        private readonly IDataSet          m_data_set   ;
        private readonly ConstructorInfo   m_ctor       ;


        private  IEnumerator<IDataStruct> m_enumerator;

        public TEntity Current
        {
            get
            {
                return (TEntity)m_ctor.Invoke(new object[] { m_enumerator.Current });
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public ObjectQueryIterator(IDataSource dataSource , IQueryBuilder query )
        {
            m_data_source = dataSource;
            m_data_set    = dataSource.GetDataSet(query);
            m_ctor        = typeof(TEntity).GetConstructor(new Type[] { typeof(IDataStruct) });

         }


        public IEnumerator<TEntity> GetEnumerator()
        {
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

      
        public bool MoveNext()
        {
            return m_enumerator.MoveNext();
        }

        public void Reset()
        {
            m_enumerator = m_data_set.GetEnumerator();
        }


        public void Dispose()
        {
            m_data_set.Dispose();
            m_data_source.Dispose();
        }

    }
}
