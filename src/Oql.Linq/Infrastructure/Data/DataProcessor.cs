using Oql.Linq.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Linq.Infrastructure.Data
{

    public class DataPermissionException : Exception
    {

    }

    public abstract class DataProcessor<T> : IDataProcessor<T>
    {


        protected abstract IDataValidationStrategy<T> CreateValidator();

        protected abstract IDisposable TransactionScope(T instance);


        protected abstract Task RunInsert(T instance);

        protected abstract Task<bool> IsAccessForInsert(T data);
        


        public async Task<IDataResult<T>> ProcessInsert(T instance)
        {

            IDataValidationStrategy<T> validator = CreateValidator();


            using (TransactionScope(instance))
            {
                if (await IsAccessForInsert(instance))
                {
                    IDataResult<T> result = validator.Validate(new ObjectChangeSet<T>(instance));

                    if (result.Ok)
                    {
                        await RunInsert     (instance);
                        await InsertComplete(instance);
                    }

                    return result;
                }
                return validator.Context.CriticalError(instance, "PermissionDenied");
            }
        }

        protected abstract Task InsertComplete(T instance);



        protected abstract Task<bool> IsAccessForUpdate(T instance);
       
        protected abstract Task ApplyUpdate(IDataChangeSet<T> changeSet);



        public async Task<IDataResult<T>> ProcessUpdate(IDataChangeSet<T> changeSet)
        {

            IDataValidationStrategy<T> validator = CreateValidator();

            using (TransactionScope(changeSet.Instance))
            {
                if (await IsAccessForUpdate(changeSet.Instance))
                {
                    IDataResult<T> result = validator.Validate(changeSet);

                    if (result.Ok)
                    {
                        await ApplyUpdate   (changeSet);
                        await UpdateComplete(changeSet);
                    }

                    return result;
                }

                return validator.Context.CriticalError(changeSet.Instance, "PermissionDenied");
            }

        }

        protected abstract Task UpdateComplete(IDataChangeSet<T> changeSet);

        protected abstract Task<bool> IsAccessForDelete(T instance);

        protected abstract Task<bool> ExecuteDelete(T instance);
      

        public async Task<bool> ProcessDelete(T instance)
        {

            using (TransactionScope(instance))
            {
                if (await IsAccessForDelete(instance))
                {
                    if (await ExecuteDelete(instance))
                    {
                        await DeleteComplete(instance);
                        return true;
                    }
                }
            }

            return false;
        }

        protected abstract Task DeleteComplete(T data);
        
    }
}
