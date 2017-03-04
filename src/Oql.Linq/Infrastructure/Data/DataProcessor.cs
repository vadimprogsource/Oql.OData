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


        protected abstract IDisposable TransactionScope(T instance);


        protected abstract void RunInsert(T data);

        protected virtual bool IsAccessForInsert(T data)
        {
            return false;
        }


        public T ProcessInsert(T instance)
        {
            if (IsAccessForInsert(instance))
            {
                using (TransactionScope(instance))
                {
                    RunInsert(instance);
                    return InsertComplete(instance);
                }
            }

            throw new DataPermissionException();
        }

        protected virtual T InsertComplete(T instance)
        {
            return instance;
        }



        protected virtual bool IsAccessForUpdate(T instance)
        {
            return true;
        }

        protected abstract void ApplyUpdate(IDataChangeSet<T> changeSet);

        public T ProcessUpdate(IDataChangeSet<T> changeSet)
        {
            if (IsAccessForUpdate(changeSet.Instance))
            {
                using (TransactionScope(changeSet.Instance))
                {
                    ApplyUpdate(changeSet);
                    return UpdateComplete(changeSet.Instance);
                }
            }

            throw new DataPermissionException();
        }

        protected virtual T UpdateComplete(T instance)
        {
            return instance;
        }

        private bool IsAccessForDelete(T instance)
        {
            return true;
        }

        protected virtual bool ExecuteDelete(T instance)
        {
            return false;
        }


        public bool ProcessDelete(T instance)
        {
            if (IsAccessForDelete(instance))
            {
                using (TransactionScope(instance))
                {

                    if (ExecuteDelete(instance))
                    {
                        DeleteComplete(instance);
                        return true;
                    }

                    return false;
                }
            }

            throw new DataPermissionException();
        }

        protected virtual T DeleteComplete(T data)
        {
            return data;
        }
    }
}
