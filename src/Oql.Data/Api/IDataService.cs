using System;
using System.Collections.Generic;
using System.Text;

namespace Oql.Data.Api
{
    public interface IDataService<T> : IDataProvider<T>, IDataProcessor<T>
    {
    }
}
