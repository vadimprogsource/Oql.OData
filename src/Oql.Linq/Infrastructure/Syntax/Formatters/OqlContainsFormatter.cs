using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlContainsFormatter : OqlBaseFormatter
    {
        public override void FormatMethodCall(IOqlExpressionVisitor visitor, MethodCallExpression methodCall)
        {
            if (methodCall.Method.DeclaringType == typeof(Enumerable))
            {
                visitor.QueryBuilder.AppendBeginExpression();
                visitor.Visit(methodCall.Arguments.First());
                visitor.QueryBuilder.AppendIn();
                visitor.Visit(methodCall.Arguments[1]);
                visitor.QueryBuilder.AppendEndExpression();

                return;
            }

            visitor.QueryBuilder.AppendBeginExpression();


            visitor.Visit(methodCall.Object);
            visitor.QueryBuilder.AppendLike();
            visitor.VisitSearchPattern(true, methodCall.Arguments.First(), true);

            visitor.QueryBuilder.AppendEndExpression();
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return new Method<string>             (x => x.Contains(string.Empty));
            yield return new Method<IEnumerable<object>>(x=>x.Contains(string.Empty));
        }
    }
}
