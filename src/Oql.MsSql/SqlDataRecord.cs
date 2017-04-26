using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oql.MsSql
{
    public class SqlDataRecord : IDataStruct
    {
        private readonly IDictionary<string, int> m_index;
        private readonly IDataRecord              m_record;


        public SqlDataRecord(IDictionary<string, int> index , IDataRecord record)
        {
            m_index  = index ;
            m_record = record; 
        }

        public object[] GetRecord(Type[] types)
        {
            object[] record = new object[types.Length];

            m_record.GetValues(record);

            for (int i = 0; i < types.Length; i++)
            {
                object value = Convert.ChangeType(record[i], types[i]);
                record[i] = value;
            }

            return record;
        }

        public T GetValueByIndex<T>(int index)
        {
            try
            {

                object rawValue = m_record.GetValue(index);

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

        public T GetValueByName<T>(string name)
        {
            int i;

            if (m_index.TryGetValue(name, out i))
            {
                return GetValueByIndex<T>(i);
            }

            return default(T);
        }
    }
}
