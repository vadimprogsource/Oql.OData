using Oql.Linq.Api.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class MethodSet : IEnumerable<MethodInfo>
    {

        private List<MethodInfo> m_methods = new List<MethodInfo>();


        public MethodSet() { }

        public MethodSet(IEnumerable<MethodInfo> methods)
        {
            m_methods.AddRange(methods);
        }


        public IEnumerator<MethodInfo> GetEnumerator()
        {
            return m_methods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_methods.GetEnumerator();
        }


        public MethodSet Add<T>(Expression<Action<T>> methodCall)
        {
            m_methods.Add((methodCall.Body as MethodCallExpression).Method);
            return this;
        }


        public MethodSet Add(IMethodInfo method)
        {
            m_methods.Add(method.GetMethodInfo());
            return this;
        }

        
    }
}
