using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class Entity : BaseMetadata ,  IEntity , IFrom
    {
        private List<Property> m_members = new List<Property>();

        public Type RuntimeType { get; set; }

        public IProperty[] Properties
        {
            get
            {
                return m_members.ToArray();
            }
        }

        public IFrom From
        {
            get
            {
                return this;
            }
        }

        public string UniqueKey => throw new NotImplementedException();

        public string ForSelect => Name;

        public string ForInsert => Name;

        public string ForUpdate => Name;

        public string ForDelete => Name;

        public IProperty this[Expression propertyOrField]
        {
            get
            {
                throw new NotImplementedException();
            }
        }




        public Entity()
        { }


        public Entity(IEnumerable<IProperty> members)
        {
            m_members = members.Select(x => new Property(x)).ToList();
        }


        public Entity(IEntity entityInfo) : base(entityInfo)
        {
            RuntimeType = entityInfo.RuntimeType;
            m_members   = entityInfo.Properties.Select(x => new Property(x)).ToList();
        }


        private void AppendProperties(IEnumerable<PropertyInfo> props)
        {
            foreach (PropertyInfo x in props)
            {
                AddProperty(x);
            }
        }

        public Entity(Type type)
        {
            BaseType = type;
            Name     = type.Name;


            if (type.IsInterface)
            {
                AppendProperties(type.GetInterfaces().SelectMany(x => x.GetProperties()));
                AppendProperties(type.GetProperties());
                return;
            }

            if (type.IsAbstract)
            {
                AppendProperties(type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(x => x.CanRead && x.GetGetMethod().IsAbstract));
            }

         
        }

        public void Add(Type type , string name)
        {
            m_members.Add(new Property { BaseType = type, Name = name });
        }


        public void AddProperty(PropertyInfo property)
        {
            m_members.Add(new Property { BaseType = property.PropertyType, Name = property.Name,BaseProperty = property });
        }

        public void AddPropertyOrField(MemberExpression propertyOrField)
        {
            Add(propertyOrField.Type, Name = propertyOrField.Member.Name);
        }
     
    }
}
