using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oql.Data.Api
{

    public interface ILookupOptions
    {
        Guid Id { get; }
        bool Can<T, V>(Expression<Func<T, V>> propertyOrField);
    }

    public interface IDataProvider<T>
    {
        Task<T> Lookup(ILookupOptions options);
        Task<IPageResult<T>> GetPage(IFilter filter);
    }
}
