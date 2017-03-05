using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public interface IDataValidationContext
    {
        bool HasErrors { get; }
        bool HasWarnings { get; }

        IDataValidationContext AddError(string message);
        IDataValidationContext AddWarning(string message);

        IDataResult<T> CriticalError<T>(T instance , string message);
    }
}
