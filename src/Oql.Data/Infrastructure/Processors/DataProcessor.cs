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


        protected abstract void RunInsert(T data);

        protected virtual bool IsAccessForInsert(T data)
        {
            return false;
        }


        public T ProcessInsert(T data)
        {
            if (IsAccessForInsert(data))
            {
                using (TransactionScope(data))
                {
                    RunInsert(data);
                    return InsertComplete(data);
                }
            }

            throw new PermissionException();
        }

        private T InsertComplete(T data)
        {
            return data;
        }



        protected virtual bool IsAccessForUpdate(T data)
        {
            return true;
        }

        protected abstract void ApplyUpdate(IChangeSet<T> data);

        public T ProcessUpdate(IChangeSet<T> data)
        {
            if (IsAccessForUpdate(data.Instance))
            {
                using (TransactionScope(data.Instance))
                {
                    ApplyUpdate(data);
                    return UpdateComplete(data);
                }
            }

            throw new PermissionException();
        }

        protected virtual T UpdateComplete(IChangeSet<T> data)
        {
            return data.Instance;
        }

        private bool IsAccessForDelete(T data)
        {
            return true;
        }

        protected virtual bool ExecuteDelete(T data)
        {
            return false;
        }


        public bool ProcessDelete(T data)
        {
            if (IsAccessForDelete(data))
            {
                using (TransactionScope(data))
                {

                    if (ExecuteDelete(data))
                    {
                        DeleteComplete(data);
                        return true;
                    }

                    return false;
                }
            }

            throw new PermissionException();
        }

        protected virtual T DeleteComplete(T data)
        {
            return data;
        }
    }
}
