using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlSelectClause : OqlBaseClause
    {

        public  Method SelectInfo   = new Method<IQueryable<object>>(x => x.Select(y => new object()));
        private Method DistinctInfo = new Method<IQueryable<object>>(x => x.Distinct());


        private bool       m_has_distinct      = false;
        private Expression m_select_expression = null;



        public override void AddMethodCall(MethodCallExpression methodCall)
        {
            if (DistinctInfo.Equals(methodCall))
            {
                m_has_distinct = true;
                return;
            }

            m_select_expression = methodCall.Arguments[1];
         
        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return SelectInfo ;
            yield return DistinctInfo;
        }


        protected virtual void VisitForSelect(IOqlExpressionVisitor visitor)
        {
            visitor.QueryBuilder.AppendSelect();

            if (m_has_distinct)
            {
                visitor.QueryBuilder.AppendDistinct();
            }


            int size = visitor.SyntaxProvider.GetTake().Size;

            if (size > 0 && size < int.MaxValue)
            {
                visitor.QueryBuilder.AppendTop(size);
            }


        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
            VisitForSelect(visitor);
            
            if (m_select_expression == null)
            {

                bool inProcess = false;

                foreach (var mi in visitor.BaseEntityType.GetMembers())
                {

                    if (mi.MemberType != MemberTypes.Property)
                    {
                        continue;
                    }



                    if (inProcess)
                    {
                        visitor.QueryBuilder.AppendExpressionSeparator();
                    }

                    inProcess = true;

                    visitor.QueryBuilder.AppendMember(mi);

                }




                return;
            }

            visitor.Visit(m_select_expression);
        }
    }
}
