using Oql.Linq.Api.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Oql.Linq.Infrastructure.Metadata;

namespace Oql.Linq.Infrastructure.CodeGen
{
    public class ExpressionBuilder : IExpressionBuilder
    {
        private ICodeProvider m_code_provider;
        public ExpressionBuilder(ICodeProvider codeProvider)
        {
            m_code_provider = codeProvider;
        }

        public LambdaExpression MakeMemberAccess(Type entityType, string name)
        {
            ParameterExpression x = Expression.Parameter(entityType, "x");
            return Expression.Lambda(Expression.PropertyOrField(x,name), x);
        }

        public LambdaExpression MakeMembersInit(Type entityType, params string[] names)
        {

            ParameterExpression x       = Expression.Parameter(entityType, "x");
            MemberExpression[]  members = new MemberExpression[names.Length];
            EntityInfo          e       = new EntityInfo();


            for (int i = 0; i < members.Length; i++)
            {
                members[i] = Expression.PropertyOrField(x,names[i]);
                e.AddPropertyOrField(members[i]);
            }

            Type resultType = m_code_provider.GetType(e);

       
            MemberAssignment[] assignments = new MemberAssignment[members.Length];


            for (int i = 0; i < members.Length; i++)
            {
                MemberExpression me = members[i];
                var              mi = resultType.GetMember(me.Member.Name);

                if (mi.Length > 0)
                {
                    assignments[i] = Expression.Bind(mi[0], me);
                }
            }

            return Expression.Lambda(Expression.MemberInit(Expression.New(resultType), assignments), x);
        }
    }
}
