using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class BaseMetadata : IMetadata
    {
        public Type BaseType { get; set; }
        public string Name { get; set; }


        public BaseMetadata() { }

        public BaseMetadata(IMetadata metadata)
        {
            BaseType = metadata.BaseType;
            Name     = metadata.Name;
        }

    }
}
