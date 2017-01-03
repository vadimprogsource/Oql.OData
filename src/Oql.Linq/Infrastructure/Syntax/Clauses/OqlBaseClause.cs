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


        public abstract void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall);
       

        public virtual IEnumerable<IMethod> GetMethods()
        {
            return Enumerable.Empty<IMethod>();
        }
        
        public abstract void VisitTo      (IOqlExpressionVisitor visitor);
       
    }
}
