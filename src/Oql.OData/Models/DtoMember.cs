using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.OData.Models
{
    public class DtoMember : DataTransferObject
    {
        public DtoMember(IProperty dataMember)
        {
            this["name"] = DtoEntity.GenerateSqlName( dataMember.Name);

            if (dataMember.Size > 0 && dataMember.BaseType == typeof(string))
            {
                this["size"] = dataMember.Size;
            }

            if (dataMember.IsPrimaryKey)
            {
                this["identity"] = true;
            }
        }
    }
}
