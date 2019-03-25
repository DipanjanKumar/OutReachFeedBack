using OutReachBusinessLayer.Events;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OutReachServiceLayer.Controllers
{
    /// <summary>
    /// Get information about Outreach Events
    /// </summary>
    [RoutePrefix("api/OutreachEvents")]
    public class EventController : ApiController
    {
        /// <summary>
        /// Get Details of all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllEvents")]
        public HttpResponseMessage Get()
        {
            try
            {
                OutReachEvent outReachEvent = new OutReachEvent();
                return Request.CreateResponse(HttpStatusCode.OK, outReachEvent.GetAllEvents());
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Get event details by event id
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEventDetails/{EventID}")]
        public HttpResponseMessage Get(string EventID)
        {
            try
            {
                OutReachEvent outReachEvent = new OutReachEvent();
                return Request.CreateResponse(HttpStatusCode.OK, outReachEvent.GetEventById(EventID));

            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}