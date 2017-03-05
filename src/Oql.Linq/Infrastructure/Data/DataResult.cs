using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Data
{
    public class DataResult<T> : IDataResult<T>,IDataValidationContext
    {
        private List<IDataError> data_errors = new List<IDataError>();


        public T Instance { get; internal set; }

        public bool Ok
        {
            get
            {
                return data_errors.Count < 1;
            }
        }

        public bool HasErrors
        {
            get
            {
                return data_errors.Any(x=>!x.IsWarning);
            }
        }

        public bool HasWarnings
        {
            get
            {
                return data_errors.Any(x => x.IsWarning);
            }
        }

        public IEnumerable<IDataError> Errors
        {
            get
            {
                return data_errors;
            }
        }


        public DataResult(T instance)
        {
            Instance = instance;
        }

        public IDataValidationContext AddError(string message)
        {
            data_errors.Add(new DataError(false, message));
            return this;
        }

        public IDataValidationContext AddWarning(string message)
        {
            data_errors.Add(new DataError(true, message));
            return this;
        }

        public IDataResult<TOuter> CriticalError<TOuter>(TOuter instance, string message)
        {
            return new DataResult<TOuter>(instance).AddError(message) as IDataResult<TOuter>;
        }
    }
}
