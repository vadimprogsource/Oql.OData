using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataIdentity
    {
        Guid Id { get; }

    }

    public interface IDataIdentity<T>
    {
        bool Can<V>(Expression<Func<T, V>> propertyOrField);
    }
}
