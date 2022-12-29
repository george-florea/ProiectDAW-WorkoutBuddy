using AutoMapper;
using Backend.Common.DTOs;
using Backend.DataAccess;

namespace Backend.BusinessLogic.Base
{
    public class ServiceDependencies
    {
        public IMapper Mapper { get; set; }
        public UnitOfWork UnitOfWork { get; set; }

        public ServiceDependencies(IMapper mapper, UnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
    }
}
