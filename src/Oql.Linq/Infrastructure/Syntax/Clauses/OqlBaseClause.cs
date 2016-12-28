using Oql.Linq.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public abstract class OqlBaseClause : IOqlClause , IOqlMethodCallEntry
    {
        public IOqlClause Clause
        {
            get
            {
                return this;
            }
        }

        public IOqlMethodCallFormatter Formatter
        {
            get
            {
                return null;
            }
        }

        public abstract void AddMethodCall(MethodCallExpression methodCall);

        public virtual IEnumerable<IMethodInfo> GetMethods()
        {
            return Enumerable.Empty<IMethodInfo>();
        }

        public abstract void VisitTo      (IOqlExpressionVisitor visitor);
       
    }
}
