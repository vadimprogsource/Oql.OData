using Oql.Linq.Api.CodeGen;
using System;
using System.Collections.Generic;
using Oql.Linq.Api.Metadata;

namespace Oql.Linq.Infrastructure.CodeGen
{
    public class CodeProvider : ICodeProvider
    {


        private class EntityEqualityComparer : IEqualityComparer<IEntity>
        {
            public bool Equals(IEntity x, IEntity y)
            {
                if (x.BaseType == y.BaseType && x.Name == y.Name)
                {
                    IProperty[] ax = x.Properties , ay = y.Properties;

                    if (ax.Length == ay.Length)
                    {
                        for (int i = 0, j = ax.Length-1; i < ax.Length && j > 0; i++, j--)
                        {
                            IProperty mx = ax[i], my = ay[i];

                            if (mx.BaseType == my.BaseType && mx.Name == my.Name)
                            {
                                mx = ax[j]; my = ay[j];

                                if (mx.BaseType == my.BaseType && mx.Name == my.Name)
                                {
                                    continue;
                                }
                            }

                            return false;
                        }

                        return true;
                    }
                }

                return false;
            }

            public int GetHashCode(IEntity obj)
            {
                int hc =  obj.GetType().GetHashCode();

                if (obj.BaseType != null)
                {
                    hc ^= obj.GetHashCode();
                }

                if (!string.IsNullOrWhiteSpace(obj.Name))
                {
                    hc ^= obj.Name.GetHashCode();
                }

                foreach (IProperty m in obj.Properties)
                {
                    hc ^= (m.BaseType.GetHashCode() ^ m.Name.GetHashCode());
                }

                return hc;
            }
        }




        private readonly IEntityCodeBuilder            m_code_builder;
        private readonly Dictionary<string, Type>      m_entity_name_cache = new Dictionary<string, Type>();
        private readonly Dictionary<IEntity, Type> m_entity_type_cache = new Dictionary<IEntity, Type>(new EntityEqualityComparer());

        public CodeProvider(IEntityCodeBuilder codeBuilder)
        {
            m_code_builder = codeBuilder;
        }


        public CodeProvider() : this(new EntityCodeBuilder()) { }


        public Type GetType(IEntity entity)
        {
            lock (this)
            {
                Type type;

                if(m_entity_type_cache.TryGetValue(entity , out type))
                {
                    return type;
                }

                m_entity_type_cache.Add(entity, type = m_code_builder.Build(entity));
                return type;
            }
        }

        public Type GetType(IMetaDataProvider provider, string entityName)
        {
            lock (this)
            {
                Type type;

                if (m_entity_name_cache.TryGetValue(entityName, out type))
                {
                    return type;
                }

                type = m_code_builder.Build(provider.GetEntity(entityName));

                m_entity_name_cache.Add(entityName, type);
                return type;
            }
        }
    }
}
