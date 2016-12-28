using Oql.Linq.Api.Metadata;
using Oql.Linq.Api.Syntax;
using Oql.Linq.Api.Syntax.Formatters;
using Oql.Linq.Infrastructure.Metadata;
using Oql.Linq.Infrastructure.Syntax.Clauses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Oql.Linq.Infrastructure.Syntax.Formatters
{
    public class OqlSyntaxProvider : IOqlSyntaxProvider
    {


        private Dictionary<MethodInfo, IOqlMethodCallEntry> m_method_calls     = new Dictionary<MethodInfo, IOqlMethodCallEntry>();
        private List<IOqlClause>                            m_ordered_clauses  = new List<IOqlClause>();
        private IOqlTakeByClause                            m_take_clause      = null;


        public IOqlSyntaxProvider Register(IOqlMethodCallEntry methodEntry)
        {
            foreach (IMethodInfo m in methodEntry.GetMethods())
            {
                m_method_calls[m.GetMethodInfo()] = methodEntry;
            }

            IOqlClause clause = methodEntry.Clause;

            if (clause != null)
            {
                m_ordered_clauses.Add(clause);

      
                if (clause is IOqlTakeByClause)
                {
                    m_take_clause = clause as IOqlTakeByClause;
                }

            }

            return this;
        }


        public IOqlBitwiseFormatter GetBitwise()
        {
            return new OqlBitwiseFormatter();
        }

        public IOqlBooleanFormatter GetBoolean()
        {
            return new OqlBooleanFormatter();
        }

        public IOqlComparisonFormatter GetComparison()
        {
            return new OqlComparisonFormatter();
        }

        public IOqlMathFormatter GetMath()
        {
            return new OqlMathFormatter();
        }

        public IOqlMethodCallEntry GetMethodCall(MethodCallExpression methodCall)
        {

            IOqlMethodCallEntry entry;

            if (m_method_calls.TryGetValue(Method.GetKeyMethod(methodCall.Method), out entry))
            {
                return entry;
            }

            return null;

        }

        public IEnumerable<IOqlClause> GetOrderedClauses()
        {
            return m_ordered_clauses;
        }



        public IOqlTakeByClause GetTake()
        {
            return m_take_clause;
        }

        
        public virtual OqlSyntaxProvider Select()
        {
            Register(new OqlSelectClause());
            return this;
        }

        public virtual OqlSyntaxProvider From()
        {
            Register(new OqlFromClause());
            return this;
        }

        public virtual OqlSyntaxProvider Where()
        {
            Register(new OqlWhereClause());
            return this;
        }

        public virtual OqlSyntaxProvider OrderBy()
        {
            Register(new OqlOrderByClause());
            return this;
        }

        public virtual OqlSyntaxProvider TakeBy()
        {
            Register(new OqlTakeByClause());
            return this;
        }


    }
}
