using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.MsSql
{
    public class SqlMetadataProvider : IMetadataProvider
    {

        private string m_connection_string;


        public SqlMetadataProvider(string connectionString)
        {
            m_connection_string = connectionString;
        }



        private class _memberInfo : IMemberInfo
        {
            private _tableInfo m_table;
            private DataRow    m_row;

            public bool IsPrimaryKey
            {
                get
                {
                    return m_table.GetIsPrimaryKey(m_row);
                }
            }

            public IEntityInfo RelatedEntity
            {
                get
                {
                    return null;
                }
            }

            public string Name
            {
                get
                {
                    return m_table.GetName(m_row);
                }
            }

            public Type BaseType
            {
                get
                {
                    return m_table.GetBaseType(m_row);
                }
            }

            public bool AllowNull
            {
                get
                {
                    return m_table.GetIsAllowDBNull(m_row);
                }
            }

            public int Size
            {
                get
                {
                    return m_table.GetSize(m_row);
                }
            }

            public _memberInfo(_tableInfo table, DataRow row)
            {
                m_table = table;
                m_row   = row;
            }
        }

        private class _tableInfo : IEntityInfo
        {
            private DataTable m_table;

            private DataColumn m_is_identity;
            private DataColumn m_is_unique;
            private DataColumn m_is_key;
            private DataColumn m_column_name;
            private DataColumn m_column_size;
            private DataColumn m_data_type;
            private DataColumn m_is_null ;

            private List<_memberInfo> m_members;

            public Type RuntimeType
            {
                get
                {
                    return null;
                }
            }

            public IMemberInfo[] Members
            {
                get
                {
                    return m_members.ToArray();
                }
            }

            public string Name
            {
                get;
                private set;
            }

            public Type BaseType
            {
                get
                {
                    return typeof(object);
                }
            }

            public IMemberInfo this[Expression propertyOrField]
            {
                get
                {
                    return null;
                }
            }

            public _tableInfo(string name , DataTable schema)
            {
                m_is_identity = schema.Columns["IsIdentity"];
                m_is_unique   = schema.Columns["IsUnique"];
                m_is_key      = schema.Columns["IsKey"];
                m_column_name = schema.Columns["ColumnName"];
                m_column_size = schema.Columns["ColumnSize"];
                m_data_type   = schema.Columns["DataType"];
                m_is_null     = schema.Columns["AllowDBNull"];

                m_table = schema;

                Name = name;

                m_members = new List<_memberInfo>();

                foreach (DataRow row in schema.Rows)
                {
                    m_members.Add(new _memberInfo(this, row));
                }
            }



            public string GetName(DataRow row)
            {
                return row[m_column_name].ToString();
            }


            public Type GetBaseType(DataRow row)
            {
                return row[m_data_type] as Type;
            }

            public bool GetIsPrimaryKey(DataRow row)
            {
                return true.Equals(row[m_is_key]) || true.Equals(row[m_is_identity]);
            }

            public bool GetIsAllowDBNull(DataRow row)
            {
                return true.Equals(row[m_is_null]);
            }

            public int GetSize(DataRow row)
            {
                return (int)row[m_column_size];
            }

        }



        public IEntityInfo GetEntity(string typeName, params IMemberInfo[] members)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connection_string))
            {

                sqlConnection.Open();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "select top 1 * from ["+typeName+"]";

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    return new _tableInfo(typeName, sqlReader.GetSchemaTable());
                }

            }
        }
    }
}
