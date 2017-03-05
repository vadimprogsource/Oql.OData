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


        protected abstract void RunInsert(T instance);

        protected virtual bool IsAccessForInsert(T data)
        {
            return false;
        }


        public IDataResult<T> ProcessInsert(T instance)
        {

            IDataValidationStrategy<T> validator = CreateValidator();


            using (TransactionScope(instance))
            {
                if (IsAccessForInsert(instance))
                {
                    IDataResult<T> result = validator.Validate(new ObjectChangeSet<T>(instance));

                    if (result.Ok)
                    {
                        RunInsert(instance);
                        InsertComplete(instance);
                    }
                }
                return validator.Context.CriticalError(instance, "PermissionDenied");
            }
        }

        protected virtual void InsertComplete(T instance)
        {
        }



        protected virtual bool IsAccessForUpdate(T instance)
        {
            return true;
        }

        protected abstract void ApplyUpdate(IDataChangeSet<T> changeSet);



        public IDataResult<T> ProcessUpdate(IDataChangeSet<T> changeSet)
        {

            IDataValidationStrategy<T> validator = CreateValidator();

            using (TransactionScope(changeSet.Instance))
            {
                if (IsAccessForUpdate(changeSet.Instance))
                {
                    IDataResult<T> result = validator.Validate(changeSet);

                    if (result.Ok)
                    {
                        ApplyUpdate   (changeSet);
                        UpdateComplete(changeSet);
                    }
                }

                return validator.Context.CriticalError(changeSet.Instance, "PermissionDenied");
            }

        }

        protected virtual void UpdateComplete(IDataChangeSet<T> changeSet)
        {
           
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

            using (TransactionScope(instance))
            {
                if (IsAccessForDelete(instance))
                {
                    if (ExecuteDelete(instance))
                    {
                        DeleteComplete(instance);
                        return true;
                    }
                }
            }

            return false;
        }

        protected virtual void DeleteComplete(T data)
        {
        }
    }
}
