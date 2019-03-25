using OutReachFeedBack.Encryption;
using System;
using System.Web.Mvc;
using OutReachFeedBack.Constants;
using OutReachFeedBack.Models;
using System.Configuration;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OutReachFeedBack.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: FeedBack
        public ActionResult Index(string FeedbackValue)
        {
            try
            {
                string decryptedValue = AESCrypt.DecryptString(FeedbackValue);
                string[] cred = decryptedValue.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                EventDTO eventDTO = GetEventDetails(cred[0]);
                string viewName = "";
                object obj = null;
                switch (cred[2])
                {
                    case Constant.Participated:
                        if (GetRegisteredFeedbackList(cred[0], cred[1]).Count == 0)
                        {
                            List<FeedbackQuestionDTO> feedbackQuestionDTOs = GetFeedbackQuestions();
                            RegisteredVolunteerFeedbackDTO registeredVolunteerFeedbackDTO = new RegisteredVolunteerFeedbackDTO();
                            registeredVolunteerFeedbackDTO.EventDate = Convert.ToDateTime(eventDTO.EventDate).ToShortDateString();
                            registeredVolunteerFeedbackDTO.EmployeeID = cred[1];
                            registeredVolunteerFeedbackDTO.EventId = cred[0];
                            registeredVolunteerFeedbackDTO.EventName = eventDTO.EventName;
                            ViewBag.QuestionCount = feedbackQuestionDTOs.Count;
                            obj = registeredVolunteerFeedbackDTO;
                            viewName = "RegisteredFeedback";
                        }
                        else
                        {
                            viewName = "AlreadySubmitted";
                        }
                            break;
                    case Constant.NotParticipated:
                        if (GetNotAteendedFeedback(cred[0], cred[1]) == null)
                        {
                            NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO = new NotAttendedVolunteerFeedbackDTO();
                            notAttendedVolunteerFeedbackDTO.EmployeeID = cred[1];
                            notAttendedVolunteerFeedbackDTO.EventDate = Convert.ToDateTime(eventDTO.EventDate).ToShortDateString();
                            notAttendedVolunteerFeedbackDTO.EventId = cred[0];
                            notAttendedVolunteerFeedbackDTO.EventName = eventDTO.EventName;
                            obj = notAttendedVolunteerFeedbackDTO;
                            viewName = "NotParticipatedFeedback";
                        }
                        else
                        {
                            viewName = "AlreadySubmitted";
                        }
                        break;
                    case Constant.UnRegistered:
                        if (GetUnRegisteredFeedback(cred[0], cred[1]) == null)
                        {
                            UnRegisteredVolunteerFeedbackDTO unRegisteredVolunteerFeedbackDTO = new UnRegisteredVolunteerFeedbackDTO();
                            unRegisteredVolunteerFeedbackDTO.EmployeeID = cred[1];
                            unRegisteredVolunteerFeedbackDTO.EventDate = Convert.ToDateTime(eventDTO.EventDate).ToShortDateString();
                            unRegisteredVolunteerFeedbackDTO.EventId = cred[0];
                            unRegisteredVolunteerFeedbackDTO.EventName = eventDTO.EventName;
                            obj = unRegisteredVolunteerFeedbackDTO;
                            viewName = "UnregisteredFeedback";
                        }
                        else
                        {
                            viewName = "AlreadySubmitted";
                        }
                        break;
                }
                if (obj != null)
                {
                    return View(viewName, obj);
                }
                else
                {
                    return View(viewName);
                }
            }
            catch (Exception)
            {
                return View("NotAuthorised");
            }

        }
        private List<FeedbackQuestionDTO> GetFeedbackQuestions()
        {
            List<FeedbackQuestionDTO> feedbackQuestionDTOs = new List<FeedbackQuestionDTO>();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/Questions";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<FeedbackQuestionDTO>>();
                    readTask.Wait();

                    feedbackQuestionDTOs = readTask.Result;
                }
            }
            return feedbackQuestionDTOs;
        }
        private NotAttendedVolunteerFeedbackDTO GetNotAteendedFeedback(string eventId, string employeeId)
        {
            NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO = new NotAttendedVolunteerFeedbackDTO();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/NotParticipated/" + eventId + "/" + employeeId + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<NotAttendedVolunteerFeedbackDTO>();
                    readTask.Wait();

                    notAttendedVolunteerFeedbackDTO = readTask.Result;
                }
            }
            return notAttendedVolunteerFeedbackDTO;
        }
        private UnRegisteredVolunteerFeedbackDTO GetUnRegisteredFeedback(string eventId, string employeeId)
        {
            UnRegisteredVolunteerFeedbackDTO unRegisteredVolunteerFeedbackDTO = new UnRegisteredVolunteerFeedbackDTO();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/Unregistered/" + eventId + "/" + employeeId + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UnRegisteredVolunteerFeedbackDTO>();
                    readTask.Wait();

                    unRegisteredVolunteerFeedbackDTO = readTask.Result;
                }
            }
            return unRegisteredVolunteerFeedbackDTO;
        }
        private List<RegisteredVolunteerFeedbackDTO> GetRegisteredFeedbackList(string eventId, string employeeId)
        {
            List<RegisteredVolunteerFeedbackDTO> registeredVolunteerFeedbackList = new List<RegisteredVolunteerFeedbackDTO>();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/Participated/" + eventId + "/" + employeeId + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<RegisteredVolunteerFeedbackDTO>>();
                    readTask.Wait();

                    registeredVolunteerFeedbackList = readTask.Result;
                }
            }
            return registeredVolunteerFeedbackList;
        }
        private EventDTO GetEventDetails(string eventId)
        {
            EventDTO eventDTO = new EventDTO();
            string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/OutreachEvents/GetEventDetails/" + eventId + "";
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(requestURI);
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EventDTO>();
                    readTask.Wait();

                    eventDTO = readTask.Result;
                }
            }
            return eventDTO;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotParticipatedFeedback(NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO)
        {
            if (GetNotAteendedFeedback(notAttendedVolunteerFeedbackDTO.EventId, notAttendedVolunteerFeedbackDTO.EmployeeID) == null)
            {
                if (ModelState.IsValid)
                {
                    string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/PostNotParticipated";
                    using (var client = new HttpClient())
                    {
                        var responseTask = client.PostAsJsonAsync(requestURI, notAttendedVolunteerFeedbackDTO);
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                        }
                    }
                    return View("ThankYou");
                }
                else
                {
                    return View(notAttendedVolunteerFeedbackDTO);
                }
            }
            else
            {
                return View("AlreadySubmitted");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnregisteredFeedback(UnRegisteredVolunteerFeedbackDTO unRegisteredVolunteerFeedbackDTO)
        {
            if (GetUnRegisteredFeedback(unRegisteredVolunteerFeedbackDTO.EventId, unRegisteredVolunteerFeedbackDTO.EmployeeID) == null)
            {
                if (ModelState.IsValid)
                {
                    string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/PostUnregistered";
                    using (var client = new HttpClient())
                    {
                        var responseTask = client.PostAsJsonAsync(requestURI, unRegisteredVolunteerFeedbackDTO);
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                        }
                    }
                    return View("ThankYou");
                }
                else
                {
                    return View(unRegisteredVolunteerFeedbackDTO);
                }
            }
            else
            {
                return View("AlreadySubmitted");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisteredFeedback(FormCollection frm)
        {
            if (GetRegisteredFeedbackList(frm["EventId"], frm["EmployeeID"]).Count == 0)
            {
                List<RegisteredVolunteerFeedbackDTO> registeredVolunteerFeedbackList = new List<RegisteredVolunteerFeedbackDTO>();
                int questionCount = Convert.ToInt32(frm["QuestionCount"]);
                for (int i = 1; i <= questionCount; i++)
                {
                    RegisteredVolunteerFeedbackDTO registeredVolunteerFeedbackDTO = new RegisteredVolunteerFeedbackDTO
                    {
                        EmployeeID = frm["EmployeeID"],
                        EventId = frm["EventId"],
                        QuestionNumber = i,
                        FeedbackText = Regex.Replace(frm["question" + i], @"\t|\n|\r", "").Trim()
                    };
                    registeredVolunteerFeedbackList.Add(registeredVolunteerFeedbackDTO);

                }
                string requestURI = ConfigurationManager.AppSettings["ApiUrl"] + "/api/PaticipantFeedback/PostRegistered";
                using (var client = new HttpClient())
                {
                    var responseTask = client.PostAsJsonAsync(requestURI, registeredVolunteerFeedbackList);
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                    }
                }
                return View("ThankYou");
            }
            else
            {
                return View("AlreadySubmitted");
            }                
        }
    }
}