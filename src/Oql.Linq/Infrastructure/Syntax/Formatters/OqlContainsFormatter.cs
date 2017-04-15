using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlContainsFormatter : OqlBaseFormatter
    {
        public override void FormatMethodCall(IOqlExpressionVisitor visitor, MethodCallExpression methodCall)
        {
            if (methodCall.Method.DeclaringType == typeof(Enumerable))
            {
                visitor.Query.AppendBeginExpression();
                visitor.Visit(methodCall.Arguments[1]);
                visitor.Query.AppendIn();
                visitor.Visit(methodCall.Arguments.First());
                visitor.Query.AppendEndExpression();
                return;
            }


            if (methodCall.Method.DeclaringType == typeof(Queryable))
            {
                visitor.Query.AppendBeginExpression();
                visitor.Visit(methodCall.Arguments[1]);
                visitor.Query.AppendIn();
                IQueryable query = methodCall.Arguments.First().GetValue() as IQueryable;
                visitor.VisitSubQuery(query.Expression);
                visitor.Query.AppendEndExpression();
                return;
            }

            visitor.Query.AppendBeginExpression();


            visitor.Visit(methodCall.Object);
            visitor.Query.AppendLike();
            visitor.VisitSearchPattern(true, methodCall.Arguments.First(), true);

            visitor.Query.AppendEndExpression();
        }


        public override IEnumerable<IMethod> GetMethods()
        {
            yield return new Method<string>             (x => x.Contains(string.Empty));
            yield return new Method<IEnumerable<object>>(x=>x.Contains(string.Empty));
            yield return new Method<IQueryable<object>> (x => x.Contains(string.Empty));
        }
    }
}
