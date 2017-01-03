using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Oql.Linq.Infrastructure.Data
{
    public class MemberInitChangeSet : IDataChangeSet
    {

        private MemberInitExpression m_member_init;


        public MemberInitChangeSet(Expression expression)
        {
            m_member_init = expression as MemberInitExpression;
        }

        private class MemberInitChange : IDataChange
        {

            private MemberAssignment m_bind;

            public MemberInitChange(MemberBinding bind)
            {
                m_bind = bind as MemberAssignment;
            }


            public Expression NewValue
            {
                get
                {
                    return m_bind.Expression;
                }
            }

            public MemberInfo PropertyOrField
            {
                get
                {
                    return m_bind.Member;
                }
            }
        }


        public object Instance
        {
            get
            {
                return m_member_init;
            }
        }

        public IEnumerator<IDataChange> GetEnumerator()
        {
            return m_member_init.Bindings.Select(x => new MemberInitChange(x)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
