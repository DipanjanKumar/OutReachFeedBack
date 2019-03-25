using AutoMapper;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System;

namespace OutReachBusinessLayer.FeedBack
{
    public class Unregistered
    {
        UnRegisteredVolunteerFeedbackRepository unRegisteredVolunteerFeedbackRepository = null;
        public Unregistered()
        {
            unRegisteredVolunteerFeedbackRepository = new UnRegisteredVolunteerFeedbackRepository();
        }
        public UnRegisteredVolunteerFeedbackDTO GetUnregisterVolunteerFeedback(string eventId, string employeeId)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UnRegisteredVolunteerFeedbackDTO, UnRegisteredVolunteerFeedback>();
                });
                IMapper iMapper = config.CreateMapper();
                return iMapper.Map<UnRegisteredVolunteerFeedback, UnRegisteredVolunteerFeedbackDTO>(unRegisteredVolunteerFeedbackRepository.GetUnRegisteredVolunteerFeedback(eventId, employeeId));
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "Unregistered",
                    ActionrName = "GetUnregisterVolunteerFeedback()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
            
        }
        public void SaveUnregisterVolunteerFeedback(UnRegisteredVolunteerFeedbackDTO unRegisteredVolunteerFeedbackDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UnRegisteredVolunteerFeedbackDTO, UnRegisteredVolunteerFeedback>().ForMember(dest => dest.Event, opt => opt.AllowNull());
            });
                IMapper iMapper = config.CreateMapper();
                unRegisteredVolunteerFeedbackRepository.SaveUnRegisteredVolunteerFeedback(iMapper.Map<UnRegisteredVolunteerFeedbackDTO, UnRegisteredVolunteerFeedback>(unRegisteredVolunteerFeedbackDTO));
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "Unregistered",
                    ActionrName = "SaveUnregisterVolunteerFeedback()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
        }
    }
}
