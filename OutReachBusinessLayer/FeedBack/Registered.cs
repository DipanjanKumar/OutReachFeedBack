using AutoMapper;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System;
using System.Collections.Generic;

namespace OutReachBusinessLayer.FeedBack
{
    public class Registered
    {
        RegisteredVolunteerFeedbackRepository RegisteredVolunteerFeedbackRepository = null;
        public Registered()
        {
            RegisteredVolunteerFeedbackRepository = new RegisteredVolunteerFeedbackRepository();
        }
        public List<RegisteredVolunteerFeedbackDTO> GetRegisteredFeedbackList(string eventId, string employeeId)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RegisteredVolunteerFeedback, RegisteredVolunteerFeedbackDTO>().ForMember(mem => mem.EventDate, opt => opt.Ignore());
                });
                IMapper iMapper = config.CreateMapper();
                return iMapper.Map<List<RegisteredVolunteerFeedback>, List<RegisteredVolunteerFeedbackDTO>>(RegisteredVolunteerFeedbackRepository.GetRegisteredFeeackList(eventId, employeeId));
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "Registered",
                    ActionrName = "GetRegisteredFeedbackList()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
        }
        public void SaveRegisterVolunteerFeedback(List<RegisteredVolunteerFeedbackDTO > feedbackList)
        {
            foreach (RegisteredVolunteerFeedbackDTO registeredVolunteerFeedbackDTO in feedbackList )
            {
                try
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<RegisteredVolunteerFeedbackDTO, RegisteredVolunteerFeedback>().ForMember(dest => dest.Event, opt => opt.AllowNull());
                    });
                    IMapper iMapper = config.CreateMapper();
                    RegisteredVolunteerFeedbackRepository.SaveRegisteredVolunteerFeedback(iMapper.Map<RegisteredVolunteerFeedbackDTO, RegisteredVolunteerFeedback>(registeredVolunteerFeedbackDTO));
                }
                catch (Exception ex)
                {
                    ExceptionLogger logger = new ExceptionLogger()
                    {
                        ControllerName = "Registered",
                        ActionrName = "SaveRegisterVolunteerFeedback()",
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
}