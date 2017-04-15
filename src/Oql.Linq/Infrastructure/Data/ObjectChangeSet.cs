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
        private class DataMemberChange : IDataChange
        {
            private object       m_instance;
            private MemberInfo   m_member  ;


            public DataMemberChange(object instance , MemberInfo property)
            {
                m_instance = instance;
                m_member   = property;
            }

            public Expression NewValue
            {
                get
                {

                    switch (m_member.MemberType)
                    {
                        case MemberTypes.Property:
                        {
                            PropertyInfo pi = m_member as PropertyInfo;
                            return Expression.Constant(pi.GetValue(m_instance), pi.PropertyType);
                                  
                        }

                        case MemberTypes.Field:
                        {
                           FieldInfo fi = m_member as FieldInfo;
                           return Expression.Constant(fi.GetValue(m_instance), fi.FieldType);
                        }
                    }

                    throw new NotSupportedException();

                }
            }

            public MemberInfo PropertyOrField { get => m_member; }

        }

        private object                m_instance;
        private HashSet<MemberInfo>   m_changed_hash =new HashSet<MemberInfo>();


        public ObjectChangeSet(object instance)
        {
            m_instance = instance; 
        }


        public bool IsModified(MemberInfo propertyOrField)
        {
            return m_changed_hash.Contains(propertyOrField);
        }

        public void SetModified(MemberInfo propertyOrField)
        {
            m_changed_hash.Add(propertyOrField);
        }


        public IDataChangeSet ChangeAllProperties()
        {
            m_changed_hash.UnionWith(m_instance.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty));
            return this;
        }

        public IDataChangeSet ChangeAllFields()
        {
            m_changed_hash.UnionWith(m_instance.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField));
            return this;
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
            return m_changed_hash.Select(x => new DataMemberChange(m_instance, x)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class ObjectChangeSet<T> : ObjectChangeSet, IDataChangeSet<T>
    {
        public ObjectChangeSet(T instance) : base(instance)
        {
        }

        T IDataChangeSet<T>.Instance
        {
            get
            {
                return (T)Instance;
            }
        }

        public bool IsModified<V>(Expression<Func<T, V>> propertyOrField)
        {
            return base.IsModified(propertyOrField.GetMemberInfo());
        }

        public void SetModified<V>(Expression<Func<T, V>> propertyOrField)
        {
            base.SetModified(propertyOrField.GetMemberInfo());
        }
    }
}
