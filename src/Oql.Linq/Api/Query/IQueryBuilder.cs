using Oql.Linq.Api.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Api
{
    public interface IQueryBuilder
    {
        IQueryBuilder AppendClause(IOqlClause clause);
        IQueryBuilder AppendToken(object token);
        IQueryBuilder AppendBlank();
        IQueryBuilder AppendExpressionSeparator();
        IQueryBuilder AppendBeginExpression();
        IQueryBuilder AppendEndExpression();
        IQueryBuilder AppendBeginArray();
        IQueryBuilder AppendArrayElementSeparator();
        IQueryBuilder AppendEndArray();
        IQueryBuilder AppendValue(Type type, object value);
        IQueryBuilder AppendNull();
        IQueryBuilder AppendAndAlso();
        IQueryBuilder AppendOrElse();
        IQueryBuilder AppendNot();
        IQueryBuilder AppendSet();
        IQueryBuilder AppendPlus();
        IQueryBuilder AppendNegate();
        IQueryBuilder AppendAdd();
        IQueryBuilder AppendSubtract();
        IQueryBuilder AppendMultiply();
        IQueryBuilder AppendDivide();
        IQueryBuilder AppendMember(MemberInfo propertyOrField);
        IQueryBuilder AppendType(Type type);
        IQueryBuilder AppendLike();
        IQueryBuilder AppendIn();
        IQueryBuilder AppendEqual();
        IQueryBuilder AppendNotEqual();
        IQueryBuilder AppendGreaterThan();
        IQueryBuilder AppendGreaterThanOrEqual();
        IQueryBuilder AppendLessThan();
        IQueryBuilder AppendLessThanOrEqual();
        IQueryBuilder AppendLikePattern(bool hasBeginWildCard, object value, bool hasEndWildCard);
        IQueryBuilder AppendPropertyOrFieldPathSeparator();
        bool IsEmpty { get; }
        IQueryBuilder Clear();
        IQueryBuilder AppendSelect();
        IQueryBuilder AppendFrom();
        IQueryBuilder AppendWhere();
        IQueryBuilder AppendOrderBy();
        IQueryBuilder AppendTop(int top);
        IQueryBuilder AppendDistinct();
        IQueryBuilder AppendAsc();
        IQueryBuilder AppendDesc();
        IQueryBuilder AppendMax();
        IQueryBuilder AppendMin();
        IQueryBuilder AppendAvg();
        IQueryBuilder AppendSum();
        IQueryBuilder AppendCount();
        IQueryBuilder AppendInsert();
        IQueryBuilder AppendUpdate();
        IQueryBuilder AppendDelete();
        IQueryBuilder AppendAssign();
        IQueryBuilder AppendValues();
    }
}
