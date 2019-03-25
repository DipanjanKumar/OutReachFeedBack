using OutReachBusinessLayer.FeedBack;
using OutReachBusinessLayer.FeedbackQuestions;
using OutReachDTO.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OutReachServiceLayer.Controllers
{
    /// <summary>
    /// Get and Save participants feedback
    /// </summary>
    [RoutePrefix("api/PaticipantFeedback")]
    public class FeedBackController : ApiController
    {
        /// <summary>
        /// Get NotParticipated feedback by EventId and employeeId
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("NotParticipated/{EventId}/{employeeId}")]
        public HttpResponseMessage Get(string EventId, string employeeId)
        {
            try
            {
                NotParticipated notParticipated = new NotParticipated();
                NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO = notParticipated.GetNotAttendedFeedback(EventId, employeeId);
                return Request.CreateResponse(HttpStatusCode.OK, notAttendedVolunteerFeedbackDTO);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Participated/{EventId}/{employeeId}")]
        public HttpResponseMessage GetReg(string EventId, string employeeId)
        {
            try
            {
                Registered registered = new Registered();
                List<RegisteredVolunteerFeedbackDTO> registeredVolunteerFeedbackDTOlist = registered.GetRegisteredFeedbackList(EventId, employeeId);
                return Request.CreateResponse(HttpStatusCode.OK, registeredVolunteerFeedbackDTOlist);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Questions")]
        public HttpResponseMessage Get()
        {
            try
            {
                Question question = new Question();
                List<FeedbackQuestionDTO> feedbackQuestionDTOs = question.GetAllQuestions();
                return Request.CreateResponse(HttpStatusCode.OK, feedbackQuestionDTOs);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Get Unregistered feedback by EventId and employeeId
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Unregistered/{EventId}/{employeeId}")]
        public HttpResponseMessage GetUn(string EventId, string employeeId)
        {
            try
            {
                Unregistered unregistered = new Unregistered();
                UnRegisteredVolunteerFeedbackDTO unRegisteredVolunteerFeedbackDTO = unregistered.GetUnregisterVolunteerFeedback(EventId, employeeId);
                return Request.CreateResponse(HttpStatusCode.OK, unRegisteredVolunteerFeedbackDTO);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Save NotParticipated feedback
        /// </summary>
        /// <param name="notAttendedVolunteerFeedbackDTO"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("PostNotParticipated")]
        public HttpResponseMessage Post(NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO)
        {
            try
            {
                NotParticipated notParticipated = new NotParticipated();
                notParticipated.SaveNotAttendedVolunteerFeedback(notAttendedVolunteerFeedbackDTO);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Save unRegistered feedback
        /// </summary>
        /// <param name="unRegisteredVolunteerFeedbackDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostUnregistered")]
        public HttpResponseMessage PostUn(UnRegisteredVolunteerFeedbackDTO unRegisteredVolunteerFeedbackDTO)
        {
            try
            {
                Unregistered unregistered = new Unregistered();
                unregistered.SaveUnregisterVolunteerFeedback(unRegisteredVolunteerFeedbackDTO);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredVolunteerFeedbackList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostRegistered")]
        public HttpResponseMessage PostRg(List<RegisteredVolunteerFeedbackDTO> registeredVolunteerFeedbackList)
        {
            try
            {
                Registered registered = new Registered();
                registered.SaveRegisterVolunteerFeedback(registeredVolunteerFeedbackList);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}