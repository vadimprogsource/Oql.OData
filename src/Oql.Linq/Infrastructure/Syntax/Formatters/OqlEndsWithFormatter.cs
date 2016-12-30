using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oql.Linq.Api;
using System.Reflection;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlEndsWithFormatter : OqlBaseFormatter
    {
        public override void FormatMethodCall(IOqlExpressionVisitor visitor, MethodCallExpression method)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(method.Object);
            visitor.Query.AppendLike();
            visitor.VisitSearchPattern(false, method.Arguments.First(), true);
            visitor.Query.AppendEndExpression();
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return new Method<string>(x => x.EndsWith(string.Empty));
        }
    }
}
