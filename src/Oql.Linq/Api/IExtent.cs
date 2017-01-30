using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api
{
    public interface IExtent
    {
        IQueryable<T> AsQueryable<T>();     
    }
}
