using OutReachBusinessLayer;
using OutReachBusinessLayer.Report;
using OutReachDTO.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OutReachServiceLayer.Controllers
{
    /// <summary>
    /// The API contains methods of how to process excel files
    /// </summary>
    [RoutePrefix("api/ProcessExcel")]
    public class ExcelController : ApiController
    {
        /// <summary>
        /// Takes excel files as input and inserts them into corresponding database tables.
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddExcelDataToDB")]
        public HttpResponseMessage Post(string[] filePaths)
        {
            try
            {
                foreach (var item in filePaths)
                {
                    ExportExcel excel = new ExportExcel();
                    excel.ReadExcelIntoDatatable(item);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// Adds an exception to the exception table in database.
        /// </summary>
        /// <param name="exceptionLoggerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddExceptionToDB")]
        public HttpResponseMessage AddException(ExceptionDTO exceptionLoggerDTO)
        {
            OutReachException outReachException = new OutReachException();
            outReachException.AddException(exceptionLoggerDTO);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CreateExcelReport/{eventID}")]
        public HttpResponseMessage ExportExcel(string eventID)
        {
            try
            {
                ExcelReport excelReport = new ExcelReport();
                string url = excelReport.GetReportDataByEventId(eventID);
                return Request.CreateResponse(HttpStatusCode.OK, url);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}