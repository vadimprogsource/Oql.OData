using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;
using System;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlSelectClause : OqlBaseClause
    {

        public  IMethod Select    = new Method<IQueryable<object>>(x => x.Select(y => new object()));
        private IMethod Distinct  = new Method<IQueryable<object>>(x => x.Distinct());
        private IMethod Min       = new Method<IQueryable<object>>(x => x.Min(y => 1));
        private IMethod Max       = new Method<IQueryable<object>>(x => x.Max(y => 1));
        private IMethod Count     = new Method<IQueryable<object>>(x => x.Count());
        private IMethod LongCount = new Method<IQueryable<object>>(x => x.LongCount());


        private bool       m_has_distinct      = false;
        private Expression m_select_expression = null;
        private Action<IOqlExpressionVisitor,Expression> visit_to_aggregate;
 

        public override void ProcessMethodCall(IOqlSyntaxContext callContext, MethodCallExpression methodCall)
        {
            if (Distinct.Equals(methodCall))
            {
                m_has_distinct = true;
                return;
            }


            m_select_expression = methodCall.GetArgument(1);


            if (m_select_expression.NodeType == ExpressionType.MemberInit || m_select_expression.NodeType == ExpressionType.New)
            {
                return;
            }


            callContext.CallResult.ResultType = methodCall.Type;

            if (methodCall.IsCalled(Min))
            {
                visit_to_aggregate = (x,y) => callContext.AggregateFormatter.FormatMin(x, y);
                return;
            }

            if (methodCall.IsCalled(Max))
            {
                visit_to_aggregate = (x, y) => callContext.AggregateFormatter.FormatMax(x, y);
                return;
            }


            if (methodCall.IsCalled("Average"))
            {
                visit_to_aggregate = (x, y) => callContext.AggregateFormatter.FormatAverage(x, y);
                return;
            }

            if (methodCall.IsCalled("Sum"))
            {
                visit_to_aggregate = (x, y) => callContext.AggregateFormatter.FormatSum(x, y);
                return;
            }


            if (methodCall.IsCalledOr(Count, LongCount))
            {
                callContext.CallResult.IsScalar = true;
            }

            visit_to_aggregate = null;

        }


        public override IEnumerable<IMethod> GetMethods()
        {
            return new[] { Min, Max, Select, Distinct,Count,LongCount}.Union(Method.GetMethodsByName(typeof(Queryable), "Sum")).Union(Method.GetMethodsByName(typeof(Queryable), "Average"));
        }


        protected virtual void VisitForSelect(IOqlExpressionVisitor visitor)
        {
            visitor.Query.AppendSelect();

            if (m_has_distinct)
            {
                visitor.Query.AppendDistinct();
            }

           
            int size = visitor.Context.CallResult.Size;

            if (size > 0 && size < int.MaxValue)
            {
                visitor.Query.AppendTop(size);
            }


        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {

            if (visit_to_aggregate != null)
            {
                visitor.Query.AppendSelect().AppendBlank();
                visit_to_aggregate(visitor, m_select_expression);
                return;
            }

            if (visitor.Context.CallResult.IsScalar)
            {
                visitor.Query.AppendSelect().AppendBlank();
                visitor.Context.AggregateFormatter.FormatCount(visitor);
                return;
            }


            if (visitor.Context.CallResult.IsAny)
            {
                visitor.Query.AppendSelect().AppendTop(1).AppendBlank().AppendMultiply();
                return;
            }

            if (visitor.Context.CallResult.IsAll)
            {
                visitor.Query.AppendSelect().AppendBlank().AppendMultiply();
                return;
            }


            VisitForSelect(visitor);
            
            if (m_select_expression == null)
            {

                IEnumerable<MemberInfo> members = visitor.SourceType.GetMembers().Where(x=>x.MemberType == MemberTypes.Property);

                if (members.Any())
                {
                    visitor.Query.AppendMember(members.First());

                    foreach (MemberInfo x in members.Skip(1))
                    {
                        visitor.Query.AppendExpressionSeparator();
                        visitor.Query.AppendMember(x);

                    }
                }

                return;

            }

            visitor.Visit(m_select_expression);
        }
    }
}
