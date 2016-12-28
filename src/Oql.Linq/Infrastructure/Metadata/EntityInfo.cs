using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class EntityInfo : BaseMetadata ,  IEntityInfo
    {
        private List<MemberInfo> m_members = new List<MemberInfo>();

        public Type RuntimeType { get; set; }

        public IMemberInfo[] Members
        {
            get
            {
                return m_members.ToArray();
            }
        }

        public IMemberInfo this[Expression propertyOrField]
        {
            get
            {
                throw new NotImplementedException();
            }
        }




        public EntityInfo()
        { }


        public EntityInfo(IEnumerable<IMemberInfo> members)
        {
            m_members = members.Select(x => new MemberInfo(x)).ToList();
        }


        public EntityInfo(IEntityInfo entityInfo) : base(entityInfo)
        {
            RuntimeType = entityInfo.RuntimeType;
            m_members   = entityInfo.Members.Select(x => new MemberInfo(x)).ToList();
        }

        public void AddPropertyOrField(MemberExpression propertyOrField)
        {
            m_members.Add(new MemberInfo { BaseType = propertyOrField.Type, Name = propertyOrField.Member.Name });
        }


     
    }
}
