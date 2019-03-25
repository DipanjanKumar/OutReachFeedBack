using OutReachFeedBack.ExceptionFilter;
using System.Web.Mvc;

namespace OutReachFeedBack
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilterAttribute());
        }
    }
}
