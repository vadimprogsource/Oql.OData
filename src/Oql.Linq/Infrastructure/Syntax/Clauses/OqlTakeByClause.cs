using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Metadata;
using Oql.Linq.Infrastructure.Syntax.Methods;

namespace Oql.Linq.Infrastructure.Syntax.Clauses
{
    public class OqlTakeByClause : OqlBaseClause , IOqlTakeByClause
    {

        private static Method SkipInfo = new Method<IQueryable<object>>(x => x.Skip(1));
        private static Method TakeInfo = new Method<IQueryable<object>>(x => x.Take(1));


        private int m_offset   = int.MinValue;
        private int m_size     = int.MinValue;

        
       
        public int Offset
        {
            get
            {
                return m_offset;
            }
        }

        public int Size
        {
            get
            {
               return m_size; 
            }
        }


        public override void AddMethodCall(MethodCallExpression methodCall)
        {
            int i = (int)methodCall.Arguments[1].GetValue();

            if (SkipInfo.Equals(methodCall))
            {
                m_offset = i; 
            }

            if (TakeInfo.Equals(methodCall))
            {
               m_size = i;
            }

        }


        public override IEnumerable<IMethodInfo> GetMethods()
        {
            yield return SkipInfo;
            yield return TakeInfo;
        }

        public override void VisitTo(IOqlExpressionVisitor visitor)
        {
        }
    }
}
