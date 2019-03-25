using OutReachFeedBack.Models;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;

namespace OutReachFeedBack.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardDTO dashboardDTO = new DashboardDTO();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/UserAndRole/GetDashboardInfo/" + ConfigurationManager.AppSettings["UserID"].ToString() + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DashboardDTO>();
                    readTask.Wait();

                    dashboardDTO = readTask.Result;
                }
            }
            return View(dashboardDTO);
        }
        
        public ActionResult DownloadExcel(string eventID)
        {
            string url = "";
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/ProcessExcel/CreateExcelReport/" + eventID + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();

                    url = readTask.Result;
                }
                else
                {
                    return View("Error");
                }
            }
            if (!string.IsNullOrEmpty(url))
            {
                string fileName = url.Substring(url.LastIndexOf("/") + 1);
                System.IO.File.Copy(ConfigurationManager.AppSettings["SourceExportPath"].ToString() + fileName, ConfigurationManager.AppSettings["DestExportPath"].ToString() + fileName, true);
                return Json(url, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(url, JsonRequestBehavior.AllowGet);
            }
        }
    }
}