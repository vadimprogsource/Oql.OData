using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Metadata
{
    public class BaseMetadata : IMetaData
    {
        public Type BaseType { get; set; }
        public string Name { get; set; }


        public BaseMetadata() { }

        public BaseMetadata(IMetaData metadata)
        {
            BaseType = metadata.BaseType;
            Name     = metadata.Name;
        }

    }
}
