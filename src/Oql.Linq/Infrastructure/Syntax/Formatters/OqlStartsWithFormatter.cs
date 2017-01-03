using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlStartsWithFormatter : OqlBaseFormatter
    {
        public override void FormatMethodCall(IOqlExpressionVisitor visitor, MethodCallExpression method)
        {
            visitor.Query.AppendBeginExpression();
            visitor.Visit(method.Object);
            visitor.Query.AppendLike();
            visitor.VisitSearchPattern(true, method.Arguments.First(),false);
            visitor.Query.AppendEndExpression();
        }


        public override IEnumerable<IMethod> GetMethods()
        {
           yield return new Method<string>(x => x.StartsWith(string.Empty));
        }
    }
}
