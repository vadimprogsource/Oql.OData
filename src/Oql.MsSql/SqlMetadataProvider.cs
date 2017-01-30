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
    public class SqlMetaDataProvider : IMetaDataProvider
    {

        private string m_connection_string;


        public SqlMetaDataProvider(string connectionString)
        {
            m_connection_string = connectionString;
        }



        private class _memberInfo : IProperty
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

            public IEntity RelatedEntity
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

            public IRelation Join
            {
                get
                {
                    return null;
                }
            }

            public IScalarExpression Expression
            {
                get
                {
                    return null;
                }
            }

            public IEntity Entity
            {
                get
                {
                    return m_table;
                }
            }

            public _memberInfo(_tableInfo table, DataRow row)
            {
                m_table = table;
                m_row   = row;
            }
        }

        private class _tableInfo : IEntity
        {
            private bool m_lower_case;
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

            public IProperty[] Properties
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

            public IFrom From
            {
                get
                {
                    return null;
                }
            }

            public IProperty this[Expression propertyOrField]
            {
                get
                {
                    return null;
                }
            }

            public _tableInfo(string name , DataTable schema,bool lowerCase)
            {

                m_lower_case = lowerCase;

                m_is_identity = schema.Columns["IsIdentity"];
                m_is_unique   = schema.Columns["IsUnique"];
                m_is_key      = schema.Columns["IsKey"];
                m_column_name = schema.Columns["ColumnName"];
                m_column_size = schema.Columns["ColumnSize"];
                m_data_type   = schema.Columns["DataType"];
                m_is_null     = schema.Columns["AllowDBNull"];

                m_table = schema;



                if (lowerCase)
                {
                    name = name.ToLowerInvariant();
                }


                Name = name;




                m_members = new List<_memberInfo>();

                foreach (DataRow row in schema.Rows)
                {
                    m_members.Add(new _memberInfo(this, row));
                }
            }



            public string GetName(DataRow row)
            {
                return m_lower_case ? row[m_column_name].ToString().ToLowerInvariant() : row[m_column_name].ToString();
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



        public IEntity GetEntity(string typeName, bool lowerCase)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connection_string))
            {

                sqlConnection.Open();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "select top 1 * from ["+typeName+"]";

                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    return new _tableInfo(typeName, sqlReader.GetSchemaTable(),lowerCase);
                }

            }
        }

        public IEntityBuilder<T> CreateEntityBuilder<T>()
        {
            return null;
        }

        public IEntityBuilder CreateEntityBuilder(Type baseType)
        {
            return null;
        }
    }
}
