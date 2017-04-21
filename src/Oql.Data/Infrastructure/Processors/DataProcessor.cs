using Oql.Data.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oql.Data.Infrastructure.Processors
{
    public abstract class DataProcessor<T> : IDataProcessor<T>
    {


        protected abstract IDisposable TransactionScope(T instance);


        protected abstract Task RunInsert(T data);

        protected abstract Task<bool> IsAccessForInsert(T data);
       
        public abstract Task<T> ProcessCreate(params object[] @params);
      
        public async Task<T> ProcessInsert(T data)
        {
            if (await IsAccessForInsert(data))
            {
                using (TransactionScope(data))
                {
                    await RunInsert(data);
                    return await InsertComplete(data);
                }
            }

            throw new PermissionException();
        }

        protected abstract Task<T> InsertComplete(T data);
      

        protected abstract Task<bool> IsAccessForUpdate(T data);
       
        protected abstract Task ApplyUpdate(IChangeSet<T> data);

        public async Task<T> ProcessUpdate(IChangeSet<T> data)
        {
            if (await IsAccessForUpdate(data.Instance))
            {
                using (TransactionScope(data.Instance))
                {
                    await ApplyUpdate(data);
                    return await UpdateComplete(data);
                }
            }

            throw new PermissionException();
        }

        protected abstract Task<T>    UpdateComplete(IChangeSet<T> data);
        protected abstract Task<bool> IsAccessForDelete(T data);
        protected abstract Task<bool> ExecuteDelete(T data);
        


        public async Task<bool> ProcessDelete(T data)
        {
            if (await IsAccessForDelete(data))
            {
                using (TransactionScope(data))
                {

                    if (await ExecuteDelete(data))
                    {
                        await DeleteComplete(data);
                        return true;
                    }

                    return false;
                }
            }

            throw new PermissionException();
        }

        protected abstract Task DeleteComplete(T data);

      
    }
}
