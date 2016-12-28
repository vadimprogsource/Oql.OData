using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Oql.Linq.Api;

namespace Oql.MsSql
{
    public class SqlDataSource : IDataSource
    {

        private SqlConnection m_connection;

        public SqlDataSource(SqlConnection connection)
        {
            m_connection = connection;
            m_connection.Open();
        }


        private SqlCommand GetCommand(IQueryBuilder query)
        {
            SqlCommand command = m_connection.CreateCommand();
            command.CommandText = query.ToString();
            return command;

        }


        public int ExecuteCommand(IQueryBuilder query)
        {
            return GetCommand(query).ExecuteNonQuery();
        }

        public IDataSet GetDataSet(IQueryBuilder query)
        {
            return new SqlDataSet(GetCommand(query).ExecuteReader());
        }

        public object GetScalar(IQueryBuilder query)
        {
            return GetCommand(query).ExecuteScalar();
        }

        public void Dispose()
        {
            m_connection.Close();
        }

    }
}
