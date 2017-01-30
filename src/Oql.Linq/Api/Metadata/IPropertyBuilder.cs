using Oql.Linq.Infrastructure.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Metadata
{

    public interface IPropertyBuilder
    {
        IScalarExpressionBuilder Compute(string expressionBody = null);
        IPropertyBuilder Name(string name);
        IPropertyBuilder MaxSize(int size);
        IPropertyBuilder AllowNull();
        IPropertyBuilder PrimaryKey();
        IPropertyBuilder InnerJoin(IEntityBuilder to, string on);
        IPropertyBuilder OuterJoin(IEntityBuilder to, string on);
    }

    public interface IPropertyBuilder<TEntity>  : IPropertyBuilder
    {
        IPropertyBuilder<TEntity> InnerJoin<TValue>(IEntityBuilder<TEntity> to, Expression<Action<TEntity, TValue>> on);
        IPropertyBuilder<TEntity> OuterJoin<TValue>(IEntityBuilder<TEntity> to, Expression<Action<TEntity, TValue>> on);
    }


}
