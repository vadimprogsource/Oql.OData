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

    public class ObjectQueryIterator<TEntity> : ObjectQueryNavigator<TEntity> , IEnumerator<TEntity>, IDisposable
    {


        private readonly static ConstructorInfo m_ctor  = typeof(TEntity).GetConstructor(new Type[] { typeof(IDataStruct)});




        private readonly IQueryBuilder     m_query_builder;
        private readonly IDataSource       m_data_source  ;
        private          IDataSet          m_data_set     ;
        private          List<TEntity>     m_result_set; 
        


     

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
            m_data_source   = dataSource;
            m_query_builder = query;

            m_data_set   = null;
            m_result_set = null;
         }


        public void Reset()
        {
        }

        public override  IEnumerator<TEntity> GetEnumerator()
        {

            if (m_result_set == null)
            {
                m_enumerator = m_data_set.GetEnumerator();
                return this;
            }

            return m_result_set.GetEnumerator();

        }

      
        public bool MoveNext()
        {
            return m_enumerator.MoveNext();
        }

    


        public void Dispose()
        {
            if (m_data_set != null)
            {
                m_data_set.Dispose();
                m_data_source.Dispose();
            }

            m_data_set = null;

        }

        public async override Task<ObjectQueryNavigator> NavigateAsync()
        {

            m_result_set = new List<TEntity>();

            m_data_set = await m_data_source.GetDataSetAsync(m_query_builder);

            for (IDataStruct x = await m_data_set.GetRecordAsync(); x != null; x = await m_data_set.GetRecordAsync())
            {
                m_result_set.Add((TEntity)m_ctor.Invoke(new object[] {x}));
            }

            Dispose();
            return this;
        }

        public override ObjectQueryNavigator Navigate()
        {
            m_data_set = m_data_source.GetDataSet(m_query_builder);
            return this;
        }
    }
}
