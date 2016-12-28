using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class MemberInfo :BaseMetadata, IMemberInfo
    {

        public bool AllowNull { get; set; }

        public int Size { get; set; }

       
        public bool IsPrimaryKey { get; set; }
       
        public IEntityInfo RelatedEntity { get; set; }


        public MemberInfo() { }

        public MemberInfo(IMemberInfo member) : base(member)
        {
            AllowNull     = member.AllowNull;
            Size          = member.Size;
            IsPrimaryKey  = member.IsPrimaryKey;
            RelatedEntity = member.RelatedEntity;
        }

    }
}
