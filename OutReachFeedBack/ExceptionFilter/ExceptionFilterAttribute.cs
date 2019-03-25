using OutReachFeedBack.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;

namespace OutReachFeedBack.ExceptionFilter
{
    public class ExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };

                ExceptionDTO logger = new ExceptionDTO()
                {
                    ControllerName = controllerName,
                    ActionrName = actionName,
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    LogDateTime = DateTime.Now
                };
                string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "api/ProcessExcel/AddExceptionToDB";
                using (var client = new HttpClient())
                {
                    var responseTask = client.PostAsJsonAsync(requestURI, logger);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        filterContext.ExceptionHandled = true;
                    }
                }
                
            }
        }
    }
}