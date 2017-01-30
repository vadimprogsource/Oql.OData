using Oql.Linq.Api.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.OData.Models
{
    public class DtoEntity : DataTransferObject
    {

        public static string GenerateSqlName(string name)
        {
            return "[" + name.ToLowerInvariant() + "]";
        }

        public DtoEntity(IEntity entity)
        {
            this["@from"] = GenerateSqlName( entity.Name);

            foreach (var x in entity.Properties)
            {

                if (x.IsPrimaryKey || x.BaseType == typeof(string))
                {
                    this[x.Name] = new DtoMember(x);
                    continue;
                }

                this[x.Name] = GenerateSqlName(x.Name);
            }
        }
    }
}
