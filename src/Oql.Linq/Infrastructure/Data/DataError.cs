using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace Oql.Linq.Infrastructure.Data
{
    public class DataError : IDataError
    {
        public bool IsWarning { get; private set; }
        public string Message { get; private set; }


        public DataError(bool isWaring, string message)
        {
            IsWarning = isWaring;
            Message   = message;
        }
    }
}
