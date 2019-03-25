using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace OutReachBusinessLayer.Mail
{
    public class OutreachMail
    {
        EventRepository eventRepository = new EventRepository();
        public void SendMailToParticipatedUser(DataTable particaipateduserTable)
        {
            try
            {
                RegisteredVolunteerRepository registeredVolunteerRepository = new RegisteredVolunteerRepository();                
                foreach (DataRow row in particaipateduserTable.Rows)
                {
                    string body = "";
                    string subject = "";
                    if (!row.IsNull("Employee ID") && !row.IsNull("Event ID"))
                    {
                        RegisteredVolunteer registeredVolunteer = registeredVolunteerRepository.GetRegisteredVolunteerByAssociateID(row["Employee ID"].ToString());
                        Event outreachEvent = eventRepository.FindEvent(row["Event ID"].ToString());

                        if (registeredVolunteer != null && outreachEvent != null)
                        {
                            string Creds = outreachEvent.EventId + "\n" + registeredVolunteer.EmployeeID + "\n" + ConstantValues.Participated;
                            string encrypt = AESCrypt.EncryptString(Creds);
                            string url = ConfigurationManager.AppSettings["URL"].ToString() + "Feedback?FeedbackValue=" + encrypt + "";

                            subject = "Feedback for {0} Dated {1}";
                            subject = string.Format(subject, outreachEvent.EventName, outreachEvent.EventDate.Value.ToShortDateString());

                            body = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/MailTemplate/Participate_Mail.html")).ReadToEnd();
                            body = body.Replace("{UserName}", string.IsNullOrEmpty(registeredVolunteer.EmployeeName) ? "-" : registeredVolunteer.EmployeeName)
                                                           .Replace("{EventName}", string.IsNullOrEmpty(outreachEvent.EventName) ? "-" : outreachEvent.EventName)
                                                           .Replace("{EventDate}", string.IsNullOrEmpty(outreachEvent.EventDate.Value.ToShortDateString()) ? "-" : outreachEvent.EventDate.Value.ToShortDateString())
                                                           .Replace("{Url}", string.IsNullOrEmpty(url) ? "-" : url);

                            SendMail(subject, body, ConfigurationManager.AppSettings["CommonMail"].ToString());
                        }                   
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "OutreachMail",
                    ActionrName = "SendMailToParticipatedUser",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
            }
            
        }
        public void SendMailToNotParticipatedUser(DataTable notparticipateduserTable)
        {
            try
            {
                foreach (DataRow row in notparticipateduserTable.Rows)
                {
                    string body = "";
                    string subject = "";
                    if (!row.IsNull("EmployeeID") && !row.IsNull("Event ID"))
                    {
                        Event outreachEvent = eventRepository.FindEvent(row["Event ID"].ToString());
                        if (outreachEvent != null)
                        {
                            string Creds = outreachEvent.EventId + "\n" + row["EmployeeID"].ToString() + "\n" + ConstantValues.NotParticipated;
                            string encrypt = AESCrypt.EncryptString(Creds);
                            string url = ConfigurationManager.AppSettings["URL"].ToString() + "Feedback?FeedbackValue=" + encrypt + "";

                            subject = "Feedback for {0} Dated {1}";
                            subject = string.Format(subject, outreachEvent.EventName, outreachEvent.EventDate.Value.ToShortDateString());

                            body = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/MailTemplate/Not_Participate_Mail.html")).ReadToEnd();
                            body = body.Replace("{UserName}", string.IsNullOrEmpty(ConfigurationManager.AppSettings[row["EmployeeID"].ToString()].ToString()) ? "-" : ConfigurationManager.AppSettings[row["EmployeeID"].ToString()].ToString())
                                                           .Replace("{EventName}", string.IsNullOrEmpty(outreachEvent.EventName) ? "-" : outreachEvent.EventName)
                                                           .Replace("{EventDate}", string.IsNullOrEmpty(outreachEvent.EventDate.Value.ToShortDateString()) ? "-" : outreachEvent.EventDate.Value.ToShortDateString())
                                                           .Replace("{Url}", string.IsNullOrEmpty(url) ? "-" : url);

                            SendMail(subject, body, ConfigurationManager.AppSettings["CommonMail"].ToString());
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "OutreachMail",
                    ActionrName = "SendMailToNotParticipatedUser",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
            }

        }
        public void SendMailToUnregisteredUser(DataTable unregistereduserTable)
        {
            try
            {
                foreach (DataRow row in unregistereduserTable.Rows)
                {
                    string body = "";
                    string subject = "";
                    if (!row.IsNull("EmployeeID") && !row.IsNull("Event ID"))
                    {
                        Event outreachEvent = eventRepository.FindEvent(row["Event ID"].ToString());
                        if (outreachEvent != null)
                        {
                            string Creds = outreachEvent.EventId + "\n" + row["EmployeeID"].ToString() + "\n" + ConstantValues.UnRegistered;
                            string encrypt = AESCrypt.EncryptString(Creds);
                            string url = ConfigurationManager.AppSettings["URL"].ToString() + "Feedback?FeedbackValue=" + encrypt + "";

                            subject = "Feedback for {0} Dated {1}";
                            subject = string.Format(subject, outreachEvent.EventName, outreachEvent.EventDate.Value.ToShortDateString());

                            body = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/MailTemplate/UnRegister_Mail.html")).ReadToEnd();
                            body = body.Replace("{UserName}", string.IsNullOrEmpty(ConfigurationManager.AppSettings[row["EmployeeID"].ToString()].ToString()) ? "-" : ConfigurationManager.AppSettings[row["EmployeeID"].ToString()].ToString())
                                                           .Replace("{EventName}", string.IsNullOrEmpty(outreachEvent.EventName) ? "-" : outreachEvent.EventName)
                                                           .Replace("{EventDate}", string.IsNullOrEmpty(outreachEvent.EventDate.Value.ToShortDateString()) ? "-" : outreachEvent.EventDate.Value.ToShortDateString())
                                                           .Replace("{Url}", string.IsNullOrEmpty(url) ? "-" : url);

                            SendMail(subject, body, ConfigurationManager.AppSettings["CommonMail"].ToString());
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "OutreachMail",
                    ActionrName = "SendMailToUnregisteredUser",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
            }
        }
        public void SendMail(string Subject, string Body, string recipientMail)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromMail"].ToString());
            var toAddress = new MailAddress(recipientMail);
            string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();
            var smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["Host"].ToString(),
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            })
            {
                try
                {
                    DisableCertificateValidation();
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        void DisableCertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) 
                {
                    return true;
                };
        }
    }
}
