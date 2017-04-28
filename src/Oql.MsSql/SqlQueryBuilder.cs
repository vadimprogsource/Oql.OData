using Oql.Linq.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace Oql.MsSql
{
    public class SqlQueryBuilder : IQueryBuilder
    {

        private StringBuilder sql = new StringBuilder();

        public bool IsEmpty
        {
            get
            {
                return sql.Length > 0;
            }
        }

        public IQueryBuilder AppendAdd()
        {
            sql.Append('+');
            return this;
        }

        public IQueryBuilder AppendAndAlso()
        {
            sql.Append(" AND ");
            return this;
        }

        public IQueryBuilder AppendArrayElementSeparator()
        {
            sql.Append(',');
            return this;
        }

        public IQueryBuilder AppendBeginArray()
        {
            sql.Append('(');
            return this;
        }

        public IQueryBuilder AppendBeginExpression()
        {
            sql.Append('(');
            return this;
        }

        public IQueryBuilder AppendBlank()
        {
            sql.Append(' ');
            return this;
        }

        public IQueryBuilder AppendClause(IOqlClause clause)
        {
            sql.Append(clause);
            return this;
        }


        public IQueryBuilder AppendDivide()
        {
            sql.Append('/');
            return this;

        }

        public IQueryBuilder AppendEndArray()
        {
            sql.Append(')');
            return this;

        }

        public IQueryBuilder AppendEndExpression()
        {
            sql.Append(')');
            return this;
        }

        public IQueryBuilder AppendEqual()
        {
            sql.Append('=');
            return this;
        }

        public IQueryBuilder AppendExpressionSeparator()
        {
            sql.Append(',');
            return this;
        }

        public IQueryBuilder AppendFrom()
        {
            sql.Append("FROM");
            return this;
        }



        public IQueryBuilder AppendGreaterThan()
        {
            sql.Append('>');
            return this;
        }

        public IQueryBuilder AppendGreaterThanOrEqual()
        {
            sql.Append(">=");
            return this;
        }

        public IQueryBuilder AppendIn()
        {
            sql.Append(" IN ");
            return this;
        }

        public IQueryBuilder AppendLessThan()
        {
            sql.Append('<');
            return this;
        }

        public IQueryBuilder AppendLessThanOrEqual()
        {
            sql.Append("<=");
            return this;
        }

        public IQueryBuilder AppendLike()
        {
            sql.Append(" LIKE ");
            return this;
        }

        public IQueryBuilder AppendLikePattern(bool hasBeginWildCard, object value, bool hasEndWildCard)
        {
            sql.Append("'");

            if (hasBeginWildCard)
            {
                sql.Append('%');
            }

            sql.Append(value);

            if (hasEndWildCard)
            {
                sql.Append('%');
            }
            sql.Append("'");

            return this;


        }

        public IQueryBuilder AppendMember(MemberInfo propertyOrField)
        {
            sql.Append('[').Append(propertyOrField.Name).Append(']');
            return this;
        }

        public IQueryBuilder AppendMember(MemberExpression propertyOrField)
        {
            sql.Append(propertyOrField.Member.Name);
            return this;
        }



        public IQueryBuilder AppendMultiply()
        {
            sql.Append('*');
            return this;
        }

        public IQueryBuilder AppendNegate()
        {
            sql.Append('-');
            return this;
        }

        public IQueryBuilder AppendNot()
        {
            sql.Append(" NOT ");
            return this;

        }

        public IQueryBuilder AppendNotEqual()
        {
            sql.Append("<>");
            return this;
        }

        public IQueryBuilder AppendNull()
        {
            sql.Append("NULL");
            return this;
        }

        public IQueryBuilder AppendOrderBy()
        {
            sql.Append("ORDER BY");
            return this;
        }

        public IQueryBuilder AppendOrElse()
        {
            sql.Append(" OR ");
            return this;
        }

        public IQueryBuilder AppendPlus()
        {
            sql.Append('+');
            return this;
        }

        public IQueryBuilder AppendPropertyOrFieldPathSeparator()
        {
            sql.Append('.');
            return this;
        }

        public IQueryBuilder AppendSelect()
        {
            sql.Append("SELECT");
            return this;
        }

        public IQueryBuilder AppendSubtract()
        {
            sql.Append('-');
            return this;
        }

        public IQueryBuilder AppendToken(object token)
        {
            sql.Append(token);
            return this;
        }

        public IQueryBuilder AppendTop(int top)
        {
            sql.Append(" TOP ").Append(top);
            return this;
        }

        public IQueryBuilder AppendType(Type type)
        {

            if (type.IsInterface && type.Name.StartsWith("I"))
            {
                sql.Append('[').Append(type.Name.Substring(1)).Append(']');
                return this;
            }

            sql.Append('[').Append(type.Name).Append(']');
            return this;
        }

        public IQueryBuilder AppendValue(Type type, object value)
        {
            if (type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid))
            {
                sql.Append('\'');
                sql.Append(value);
                sql.Append('\'');
                return this;
            }

            sql.Append(value);
            return this;
        }

        public IQueryBuilder AppendDistinct()
        {
            sql.Append(" DISTINCT");
            return this;
        }
        public IQueryBuilder AppendWhere()
        {
            sql.Append("WHERE");
            return this;
        }

        public IQueryBuilder Clear()
        {
            sql.Length = 0;
            return this;
        }

        public override string ToString()
        {
            return sql.ToString();
        }

        public IQueryBuilder AppendAsc()
        {
            sql.Append(" ASC ");
            return this;
        }

        public IQueryBuilder AppendDesc()
        {
            sql.Append(" DESC ");
            return this;
        }

        public IQueryBuilder AppendMax()
        {
            sql.Append("MAX");
            return this;
        }

        public IQueryBuilder AppendMin()
        {
            sql.Append("MIN");
            return this;
        }

        public IQueryBuilder AppendAvg()
        {
            sql.Append("AVG");
            return this;
        }

        public IQueryBuilder AppendSum()
        {
            sql.Append("SUM");
            return this;
        }

        public IQueryBuilder AppendCount()
        {
            sql.Append("COUNT");
            return this;
        }

        public IQueryBuilder AppendInsert()
        {
            sql.Append("INSERT INTO");
            return this;
        }

        public IQueryBuilder AppendUpdate()
        {
            sql.Append("UPDATE");
            return this;
        }

        public IQueryBuilder AppendDelete()
        {
            sql.Append("DELETE");
            return this;
        }

        public IQueryBuilder AppendSet()
        {
            sql.Append("SET");
            return this;
        }

        public IQueryBuilder AppendAssign()
        {
            sql.Append('=');
            return this;
        }

        public IQueryBuilder AppendValues()
        {
            sql.Append("VALUES");
            return this;
        }
    }
}
