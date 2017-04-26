using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class Property :BaseMetadata, IProperty
    {

        public bool AllowNull { get; set; }

        public int Size { get; set; }

       
        public bool IsPrimaryKey { get; set; }
       
        public IEntity RelatedEntity { get; set; }

        public IRelation Join
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IScalarExpression Expression
        {
            get;
            set;
        }

        public IEntity Entity
        {
            get;
            set;
        }

        public PropertyInfo BaseProperty { get; internal set; }

        public Property() { }

        public Property(IProperty member) : base(member)
        {
            AllowNull     = member.AllowNull;
            Size          = member.Size;
            IsPrimaryKey  = member.IsPrimaryKey;
            BaseProperty  = member.BaseProperty;
        
        }

    }
}
