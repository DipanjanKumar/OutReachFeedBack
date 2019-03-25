using OutReachFeedBack.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;

namespace OutReachFeedBack.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        public ActionResult Index()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/UserAndRole/GetUserDetails/" + ConfigurationManager.AppSettings["UserID"].ToString() + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<UserDTO>>();
                    readTask.Wait();

                    userDTOs = readTask.Result;
                }
                else
                {
                    return View("Error");
                }
            }
            UserDTO userDTO = new UserDTO();
            userDTO.AssociateName = userDTOs[0].AssociateName;

            return View(userDTO);
        }
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult LogOff()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult NavigationMenu()
        {
            try
            {
                string AssociateID = ConfigurationManager.AppSettings["UserID"].ToString();
                List<UserMenuDTO> menuitemList = new List<UserMenuDTO>();
                string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "api/UserAndRole/GetUserMenu/" + AssociateID + "";
                using (var client = new HttpClient())
                {
                    var responseTask = client.GetAsync(requestURI);
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<UserMenuDTO>>();
                        readTask.Wait();

                        menuitemList = readTask.Result;
                    }
                }
                return PartialView("_MenuPartial", menuitemList);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }        
    }
}