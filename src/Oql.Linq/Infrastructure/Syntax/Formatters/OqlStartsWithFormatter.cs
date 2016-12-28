using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlStartsWithFormatter : OqlBaseFormatter
    {
        public override void FormatMethodCall(IOqlExpressionVisitor visitor, MethodCallExpression method)
        {
            visitor.QueryBuilder.AppendBeginExpression();
            visitor.Visit(method.Object);
            visitor.QueryBuilder.AppendLike();
            visitor.VisitSearchPattern(true, method.Arguments.First(),false);
            visitor.QueryBuilder.AppendEndExpression();
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
           yield return new Method<string>(x => x.StartsWith(string.Empty));
        }
    }
}
