using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataError
    {
        bool IsWarning { get; }
        string Message { get; }
    }
}
