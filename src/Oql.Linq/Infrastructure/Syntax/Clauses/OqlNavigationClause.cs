using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlNavigationClause : OqlBaseClause 
    {


        private static IMethod FirstWithPredicate              = new Method<IQueryable<object>>(x => x.First(y => true));
        private static IMethod FirstWithoutPredicate           = new Method<IQueryable<object>>(x => x.First());
        private static IMethod FirstOrDefaultWithPredicate     = new Method<IQueryable<object>>(x => x.FirstOrDefault(y => true));
        private static IMethod FirstOrDefaultWithoutPredicate  = new Method<IQueryable<object>>(x => x.FirstOrDefault(y => true));
        private static IMethod SingleWithPredicate             = new Method<IQueryable<object>>(x => x.Single(y => true));
        private static IMethod SingleWithoutPredicate          = new Method<IQueryable<object>>(x => x.Single());
        private static IMethod SingleOrDefaultWithPredicate    = new Method<IQueryable<object>>(x => x.SingleOrDefault(y => true));
        private static IMethod SingleOrDefaultWithoutPredicate = new Method<IQueryable<object>>(x => x.SingleOrDefault());
        private static IMethod LastWithPredicate               = new Method<IQueryable<object>>(x => x.Last(y => true));
        private static IMethod LastWithoutPredicate            = new Method<IQueryable<object>>(x => x.Last());
        private static IMethod LastOrDefaultWithPredicate      = new Method<IQueryable<object>>(x => x.LastOrDefault(y => true));
        private static IMethod LastOrDefaultWithoutPredicate   = new Method<IQueryable<object>>(x => x.LastOrDefault());
        private static IMethod AllWithPredicate                = new Method<IQueryable<object>>(x => x.All(y => true));
        private static IMethod AnyWithPredicate                = new Method<IQueryable<object>>(x => x.Any(y => true));
        private static IMethod AnyWithoutPredicate             = new Method<IQueryable<object>>(x => x.Any());


        private static IMethod ElementAt          = new Method<IQueryable<object>>(x => x.ElementAt(1));
        private static IMethod ElementAtOrDefault = new Method<IQueryable<object>>(x => x.ElementAt(1));

        private static IMethod Skip = new Method<IQueryable<object>>(x => x.Skip(1));
        private static IMethod Take = new Method<IQueryable<object>>(x => x.Take(1));

        private static IMethod CountWithPredicate     = new Method<IQueryable<object>>(x => x.Count    (y=>true));
        private static IMethod LongCountWithPredicate = new Method<IQueryable<object>>(x => x.LongCount(y=>true));


        public static IEnumerable<IMethod> WithPredicates()
        {
            yield return AnyWithPredicate;
            yield return FirstWithPredicate;
            yield return FirstOrDefaultWithPredicate;
            yield return SingleWithPredicate;
            yield return SingleOrDefaultWithPredicate;
            yield return LastWithPredicate;
            yield return LastOrDefaultWithPredicate;
            yield return CountWithPredicate;
            yield return LongCountWithPredicate;
        }


        public static void ProcessNavigate(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {

            callContext.CallResult.ResultType = methodCall.Type;

            if (methodCall.IsCalledOr(AnyWithPredicate,AnyWithoutPredicate))
            {
                callContext.CallResult.IsAny      = true;
                return;
            }


            if (methodCall.IsCalledOr(FirstWithPredicate,FirstWithoutPredicate))
            {
                callContext.CallResult.HasFirst = true;
                return;
            }

            if (methodCall.IsCalledOr(FirstOrDefaultWithPredicate, FirstOrDefaultWithoutPredicate))
            {
                callContext.CallResult.HasFirst = true;
                return;
            }

            if (methodCall.IsCalledOr(SingleWithPredicate, SingleWithoutPredicate))
            {
                callContext.CallResult.HasSingle = true;
                return;
            }

            if (methodCall.IsCalledOr(SingleOrDefaultWithPredicate, SingleOrDefaultWithoutPredicate))
            {
                callContext.CallResult.HasSingle = true;
                callContext.CallResult.CanDefault = true;
                return;
            }

            if (methodCall.IsCalledOr(LastWithPredicate, LastWithoutPredicate))
            {
                callContext.CallResult.HasLast = true;
                return;
            }

            if (methodCall.IsCalledOr(LastOrDefaultWithPredicate, LastOrDefaultWithoutPredicate))
            {
                callContext.CallResult.HasLast   = true;
                callContext.CallResult.CanDefault = true;
                return;
            }

            if (methodCall.IsCalledOr(CountWithPredicate, LongCountWithPredicate))
            {
                callContext.CallResult.IsScalar = true;
                return;
            }

        }

        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {

            if (methodCall.IsCalled(Skip))
            {
                callContext.CallResult.Offset = (int)methodCall.GetArgument(1).GetValue();
                return;
            }

            if(methodCall.IsCalled(Take))
            {
                callContext.CallResult.Size = (int)methodCall.GetArgument(1).GetValue();
                return;
            }

            if (methodCall.IsCalled(ElementAt))
            {
                callContext.CallResult.ElementIndex = (int)methodCall.GetArgument(1).GetValue();
                return;
            }

            if (methodCall.IsCalled(ElementAtOrDefault))
            {
                callContext.CallResult.ElementIndex = (int)methodCall.GetArgument(1).GetValue();
                callContext.CallResult.CanDefault   = true;
                return;
            }

            ProcessNavigate(callContext, methodCall);

        }


        public override IEnumerable<IMethod> GetMethods()
        {
            yield return AnyWithoutPredicate;
            yield return FirstWithoutPredicate;
            yield return FirstOrDefaultWithoutPredicate;
            yield return SingleWithoutPredicate;
            yield return SingleOrDefaultWithoutPredicate;
            yield return LastWithoutPredicate;
            yield return LastOrDefaultWithoutPredicate;
            yield return Skip;
            yield return Take;
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
        }
    }
}
