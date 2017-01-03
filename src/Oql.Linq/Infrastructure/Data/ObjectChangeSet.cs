using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Data
{
    public class ObjectChangeSet : IDataChangeSet
    {
        private class PropertyChange : IDataChange
        {
            private object       m_instance;
            private PropertyInfo m_property;


            public PropertyChange(object instance , PropertyInfo property)
            {
                m_instance = instance;
                m_property = property;
            }

            public Expression NewValue
            {
                get
                {
                    return Expression.Constant(m_property.GetValue(m_instance), m_property.PropertyType);
                }
            }

            public MemberInfo PropertyOrField
            {
                get
                {
                    return m_property;
                }
            }
        }

        private object m_instance;


        public ObjectChangeSet(object instance)
        {
            m_instance = instance; 
        }

        public object Instance 
        {
            get
            {
                return m_instance;
            }
        }

        public IEnumerator<IDataChange> GetEnumerator()
        {
            return m_instance.GetType().GetProperties().Select(x => new PropertyChange(m_instance, x)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
