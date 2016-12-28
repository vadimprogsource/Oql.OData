using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Linq.Api.CodeGen
{
    public interface IExpressionBuilder
    {
        LambdaExpression MakeMemberAccess(Type entityType, string name);
        LambdaExpression MakeMembersInit(Type entityType, params string[] names);
    }
}
