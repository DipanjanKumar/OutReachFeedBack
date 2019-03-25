using OutReachDTO.DTO;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using AutoMapper;

namespace OutReachBusinessLayer
{
    public class OutReachException
    {
        ExceptionRepository ExceptionRepository = null;
        public OutReachException()
        {
            ExceptionRepository = new ExceptionRepository();
        }
        public void AddException(ExceptionDTO loggerDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ExceptionDTO, ExceptionLogger>();
            });
            IMapper iMapper = config.CreateMapper();
            ExceptionRepository.AddException(iMapper.Map<ExceptionDTO, ExceptionLogger>(loggerDTO));
        }
    }
}
