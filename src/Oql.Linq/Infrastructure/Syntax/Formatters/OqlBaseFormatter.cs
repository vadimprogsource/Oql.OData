using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public abstract class OqlBaseFormatter : IOqlMethodCallFormatter, IOqlMethodCallEntry
    {
        public IOqlClause Clause
        {
            get
            {
                return null;
            }
        }

        public IOqlMethodCallFormatter Formatter
        {
            get
            {
                return this;
            }
        }

        public abstract void FormatMethodCall(IOqlExpressionVisitor visitor, MethodCallExpression method);
       

        public virtual  IEnumerable<IMethod> GetMethods()
        {
            return Enumerable.Empty<IMethod>();
        }
    }
}
