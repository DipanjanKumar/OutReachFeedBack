using AutoMapper;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System;
using System.Collections.Generic;

namespace OutReachBusinessLayer.FeedbackQuestions
{
    public class Question
    {
        public List<FeedbackQuestionDTO> GetAllQuestions()
        {
            try
            {
                FeedBackQuestionRepository feedBackQuestionRepository = new FeedBackQuestionRepository();
                List<FeedbackQuestion> questions = feedBackQuestionRepository.GetQuestions();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FeedbackQuestionDTO, FeedbackQuestion>();
                });
                IMapper iMapper = config.CreateMapper();
                return iMapper.Map<List<FeedbackQuestion>, List<FeedbackQuestionDTO>>(questions);
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "Question",
                    ActionrName = "GetAllQuestions()",
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
