using OutReachBusinessLayer.Excel;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using OutReachDTO.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace OutReachBusinessLayer.Report
{
    public class ExcelReport
    {
        readonly EventRepository eventRepository = null;
        readonly RegisteredVolunteerFeedbackRepository RegisteredVolunteerFeedbackRepository = null;
        readonly NotAttendedVolunteerFeedbackRepository notAttendedVolunteerFeedbackRepository = null;
        readonly UnRegisteredVolunteerFeedbackRepository unRegisteredVolunteerFeedbackRepository = null;
        readonly FeedBackQuestionRepository feedBackQuestionRepository = null;
        public ExcelReport()
        {
            eventRepository = new EventRepository();
            RegisteredVolunteerFeedbackRepository = new RegisteredVolunteerFeedbackRepository();
            notAttendedVolunteerFeedbackRepository = new NotAttendedVolunteerFeedbackRepository();
            unRegisteredVolunteerFeedbackRepository = new UnRegisteredVolunteerFeedbackRepository();
            feedBackQuestionRepository = new FeedBackQuestionRepository();
        }
        public string GetReportDataByEventId(string EventId)
        {
            try
            {
                ReportDTO reportDTO = new ReportDTO
                {
                    RegisteredReportDTOs = new List<RegisteredReportDTO>(),
                    NotAttendedReportDTOs = new List<NotAttendedReportDTO>(),
                    UnregisteredReportDTOs = new List<UnregisteredReportDTO>()
                };
                List<FeedbackQuestion> questions = feedBackQuestionRepository.GetQuestions();
                string reporttemplatePath = ConfigurationManager.AppSettings["ReportTemplate"].ToString();
                string exportPath = ConfigurationManager.AppSettings["ExportPath"].ToString();
                Event evt = eventRepository.FindEvent(EventId);
                string url = "";
                if (evt != null)
                {
                    if (questions != null && questions.Count > 0)
                    {
                        List<RegisteredVolunteerFeedback> registeredVolunteerFeedbackList = RegisteredVolunteerFeedbackRepository.GetRegisteredFeeackListByEvent(EventId);
                        if (registeredVolunteerFeedbackList != null && registeredVolunteerFeedbackList.Count > 0)
                        {
                            int loopCount = registeredVolunteerFeedbackList.Count / questions.Count;
                            int ansCount = 0;
                            for (int i = 0; i < loopCount; i++)
                            {
                                RegisteredReportDTO registeredReportDTO = new RegisteredReportDTO
                                {
                                    EventName = evt.EventName,
                                    BeneficaryName = evt.BeneficaryName,
                                    EventDate = Convert.ToDateTime(evt.EventDate).ToShortDateString(),
                                    Location = evt.Location,
                                    EmployeeID = registeredVolunteerFeedbackList[ansCount].EmployeeID,
                                    FeedbackTextNumber1 = registeredVolunteerFeedbackList[ansCount].FeedbackText,
                                    FeedbackTextNumber2 = registeredVolunteerFeedbackList[ansCount + 1].FeedbackText,
                                    FeedbackTextNumber3 = registeredVolunteerFeedbackList[ansCount + 2].FeedbackText
                                };
                                reportDTO.RegisteredReportDTOs.Add(registeredReportDTO);
                                ansCount += questions.Count;
                            }
                        }
                    }
                    List<NotAttendedVolunteerFeedback> notAttendedVolunteerFeedbackList = notAttendedVolunteerFeedbackRepository.GetNotAttendedVolunteerFeedbackList(EventId);
                    if (notAttendedVolunteerFeedbackList != null && notAttendedVolunteerFeedbackList.Count > 0)
                    {
                        foreach (NotAttendedVolunteerFeedback notAttendedVolunteerFeedback in notAttendedVolunteerFeedbackList)
                        {
                            NotAttendedReportDTO notAttendedReportDTO = new NotAttendedReportDTO();
                            notAttendedReportDTO.EventName = evt.EventName;
                            notAttendedReportDTO.BeneficaryName = evt.BeneficaryName;
                            notAttendedReportDTO.EventDate = Convert.ToDateTime(evt.EventDate).ToShortDateString();
                            notAttendedReportDTO.Location = evt.Location;
                            notAttendedReportDTO.EmployeeID = notAttendedVolunteerFeedback.EmployeeID;
                            notAttendedReportDTO.FeedbackText = notAttendedVolunteerFeedback.FeedbackText;
                            reportDTO.NotAttendedReportDTOs.Add(notAttendedReportDTO);
                        }
                    }
                    List<UnRegisteredVolunteerFeedback> unRegisteredVolunteerFeedbackList = unRegisteredVolunteerFeedbackRepository.GetUnRegisteredVolunteerFeedbackList(EventId);
                    if (unRegisteredVolunteerFeedbackList != null && unRegisteredVolunteerFeedbackList.Count > 0)
                    {
                        foreach (UnRegisteredVolunteerFeedback unRegisteredVolunteerFeedback in unRegisteredVolunteerFeedbackList)
                        {
                            UnregisteredReportDTO unregisteredReportDTO = new UnregisteredReportDTO();
                            unregisteredReportDTO.EventName = evt.EventName;
                            unregisteredReportDTO.BeneficaryName = evt.BeneficaryName;
                            unregisteredReportDTO.EventDate = Convert.ToDateTime(evt.EventDate).ToShortDateString();
                            unregisteredReportDTO.Location = evt.Location;
                            unregisteredReportDTO.EmployeeID = unRegisteredVolunteerFeedback.EmployeeID;
                            unregisteredReportDTO.FeedbackText = unRegisteredVolunteerFeedback.FeedbackText;
                            reportDTO.UnregisteredReportDTOs.Add(unregisteredReportDTO);
                        }
                    }
                    if (reportDTO.RegisteredReportDTOs.Count > 0 || reportDTO.NotAttendedReportDTOs.Count > 0 || reportDTO.UnregisteredReportDTOs.Count > 0)
                    {
                        CreateExcel createExcel = new CreateExcel();
                        url = createExcel.WriteToExcel(reportDTO, reporttemplatePath, exportPath, evt.EventName);
                    }                    
                }
                return url;
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "ExcelReport",
                    ActionrName = "GetReportDataByEventId",
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