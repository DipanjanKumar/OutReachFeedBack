using AutoMapper;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System;

namespace OutReachBusinessLayer.FeedBack
{
    public class NotParticipated
    {
        NotAttendedVolunteerFeedbackRepository notAttendedVolunteerFeedbackRepository = null;
        public NotParticipated()
        {
            notAttendedVolunteerFeedbackRepository = new NotAttendedVolunteerFeedbackRepository();
        }
        public NotAttendedVolunteerFeedbackDTO GetNotAttendedFeedback(string eventId,string employeeId)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NotAttendedVolunteerFeedback, NotAttendedVolunteerFeedbackDTO>().ForMember(mem => mem.EventDate, opt => opt.Ignore());
                });
                IMapper iMapper = config.CreateMapper();
                return iMapper.Map<NotAttendedVolunteerFeedback, NotAttendedVolunteerFeedbackDTO>(notAttendedVolunteerFeedbackRepository.GetNotAttendedVolunteerFeedback(eventId, employeeId));
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "NotParticipated",
                    ActionrName = "GetNotAttendedFeedback()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }            
        }
        public void SaveNotAttendedVolunteerFeedback(NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO)
        {
            
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NotAttendedVolunteerFeedbackDTO, NotAttendedVolunteerFeedback>().ForMember(dest => dest.Event, opt => opt.AllowNull());
                });
                IMapper iMapper = config.CreateMapper();
                notAttendedVolunteerFeedbackRepository.SaveNotAttendedVolunteerFeedback(iMapper.Map<NotAttendedVolunteerFeedbackDTO, NotAttendedVolunteerFeedback>(notAttendedVolunteerFeedbackDTO));
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "NotParticipated",
                    ActionrName = "SaveNotAttendedVolunteerFeedback()",
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
