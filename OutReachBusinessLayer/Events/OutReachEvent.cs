using AutoMapper;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System.Collections.Generic;
using System;

namespace OutReachBusinessLayer.Events
{
    public class OutReachEvent
    {
        public OutReachEvent()
        {

        }
        public List<EventDTO> GetAllEvents()
        {
            try
            {
                EventRepository eventRepository = new EventRepository();
                List<Event> events = eventRepository.GetEventList();
                
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventDTO, Event>();
                });
                IMapper iMapper = config.CreateMapper();
                return iMapper.Map<List<Event>, List<EventDTO>>(events);
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "OutReachEvent",
                    ActionrName = "GetAllEvents()",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
        }
        public EventDTO GetEventById(string EventID)
        {
            try
            {
                EventRepository eventRepository = new EventRepository();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EventDTO, Event>();
                });
                IMapper iMapper = config.CreateMapper();
                return iMapper.Map<Event, EventDTO>(eventRepository.FindEvent(EventID));
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "OutReachEvent",
                    ActionrName = "GetEventById()",
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
