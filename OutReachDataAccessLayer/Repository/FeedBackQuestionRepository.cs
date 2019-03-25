using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class FeedBackQuestionRepository
    {
        private readonly IGenericRepository<FeedbackQuestion> IFeedBackQuestionRepository = null;
        public FeedBackQuestionRepository()
        {
            IFeedBackQuestionRepository = new GenericRepository<FeedbackQuestion>();
        }
        public List<FeedbackQuestion> GetQuestions()
        {
            try
            {
                List<FeedbackQuestion> questions = IFeedBackQuestionRepository.SelectAll().ToList();
                return questions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
