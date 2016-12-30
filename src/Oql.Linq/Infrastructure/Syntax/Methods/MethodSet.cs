using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Syntax.Methods
{
    public class MethodSet : IEnumerable<IOqlClause>
    {


        private Dictionary<MethodInfo, IOqlMethodCallEntry> m_method_calls    = new Dictionary<MethodInfo, IOqlMethodCallEntry>();
        private List<IOqlClause>                            m_ordered_clauses = new List<IOqlClause>();



        public TMethodCall Call<TMethodCall>() where TMethodCall : IOqlMethodCallEntry, new()
        {

            TMethodCall methodEntry = new TMethodCall();

            foreach (IMethodInfo m in methodEntry.GetMethods())
            {
                m_method_calls[m.GetMethodInfo()] = methodEntry;
            }

            IOqlClause clause = methodEntry.Clause;

            if (clause != null)
            {
                m_ordered_clauses.Add(clause);
            }

            return methodEntry;

        }


        public IOqlMethodCallEntry this[MethodCallExpression methodCall]
        {
            get
            {
                IOqlMethodCallEntry entry;

                if (m_method_calls.TryGetValue(Method.GetKeyMethod(methodCall.Method), out entry))
                {
                    return entry;
                }

                return null;
            }
        }


        public IEnumerator<IOqlClause> GetEnumerator()
        {
            return m_ordered_clauses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }







    }
}
