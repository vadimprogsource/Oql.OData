using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Api.Data
{
    public  interface IDataValidationStrategy<T>
    {
        IDataValidationContext Context { get; }
        IDataResult<T> Validate(IDataChangeSet<T> changeSet);

    }
}
