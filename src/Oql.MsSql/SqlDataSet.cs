using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;

namespace Oql.MsSql
{
    public class SqlDataSet : IDataSet , IEnumerator<IDataStruct>, IDataStruct
    {
        private Dictionary<string, int> m_fields;
        private SqlDataReader           m_reader;


        public SqlDataSet(SqlDataReader reader)
        {
            m_fields = null;
            m_reader = reader;
        }

        public IDataStruct Current
        {
            get
            {
                return this;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this;
            }
        }

        public IEnumerator<IDataStruct> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            m_reader.Dispose();
        }

        public bool MoveNext()
        {
            return m_reader.Read();
        }

        public void Reset()
        {
           
        }

        

        public object[] GetRecord(Type[] types)
        {
            object[] record = new object[types.Length];

            m_reader.GetValues(record);


            for(int i = 0; i< types.Length;i++)
            {
                object value = Convert.ChangeType(record[i], types[i]);
                record[i] = value;
            }

            return record;
        }


        private int GetIndex(string name)
        {
            if (m_fields == null)
            {
                m_fields = new Dictionary<string, int>();

                for (int i = 0; i < m_reader.FieldCount; i++)
                {
                    m_fields.Add(m_reader.GetName(i), i);
                }
            }


            int index;

            if (m_fields.TryGetValue(name, out index))
            {
                return index;
            }

            throw new IndexOutOfRangeException();
        }

        private T GetValue<T>(int index)
        {
            try
            {

                object rawValue = m_reader.GetValue(index);

                if (rawValue == null || rawValue.GetType() == typeof(DBNull))
                {
                    return default(T);
                }

                return (T)Convert.ChangeType(rawValue, typeof(T));

            }
            catch
            {
                return default(T);
            }
        }


       
        public T GetValueByIndex<T>(int index)
        {
            return GetValue<T>(index);
        }

        public T GetValueByName<T>(string name)
        {
            return GetValue<T>(GetIndex(name));
        }
    }
}
