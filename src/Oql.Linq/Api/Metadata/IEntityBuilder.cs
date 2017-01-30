using Oql.Linq.Infrastructure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{

    public interface IEntityBuilder
    {
        IEntityBuilder BaseType(Type type);
        IEntityBuilder BaseEntity(IEntityBuilder baseEntity);
        IEntityBuilder ForSelect(string from);
        IEntityBuilder ForInsert(string from);
        IEntityBuilder ForUpdate(string from);
        IEntityBuilder ForDelete(string from);
        IPropertyBuilder Property(MemberInfo propertyOrField);
        IPropertyBuilder Property(string propertyOrFieldName);

        IEntity MakeEntity();
    }


    public interface IEntityBuilder<TEntity>  : IEntityBuilder
    {
        IPropertyBuilder<TEntity> Property<TValue>(Expression<Func<TEntity, TValue>> propertyOrField);
    }
}
