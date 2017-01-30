using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class Entity : BaseMetadata ,  IEntity
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
                return null;
            }
        }

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

        public void AddPropertyOrField(MemberExpression propertyOrField)
        {
            m_members.Add(new Property { BaseType = propertyOrField.Type, Name = propertyOrField.Member.Name });
        }


     
    }
}
