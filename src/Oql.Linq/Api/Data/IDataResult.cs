using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataResult<T>
    {

        bool HasErrors { get; }
        bool HasWarnings { get; }

        bool Ok { get; }

        IEnumerable<IDataError> Errors { get; }
    }
}
