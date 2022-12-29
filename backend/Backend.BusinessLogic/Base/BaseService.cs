using AutoMapper;
using Backend.Common.DTOs;
using Backend.DataAccess;
using System;
using System.Transactions;

namespace Backend.BusinessLogic.Base
{
    public class BaseService
    {
        protected readonly IMapper Mapper;
        protected readonly UnitOfWork UnitOfWork;

        public BaseService(ServiceDependencies serviceDependencies)
        {
            Mapper = serviceDependencies.Mapper;
            UnitOfWork = serviceDependencies.UnitOfWork;
        }

        protected TResult ExecuteInTransaction<TResult>(Func<UnitOfWork, TResult> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            using (var transactionScope = new TransactionScope())
            {
                var result = func(UnitOfWork);

                transactionScope.Complete();

                return result;
            }
        }

        protected void ExecuteInTransaction(Action<UnitOfWork> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using (var transactionScope = new TransactionScope())
            {
                action(UnitOfWork);

                transactionScope.Complete();
            }
        }
    }
}
