using OutReachBusinessLayer.Mail;
using OutReachDataAccessLayer.Models;
using OutReachDataAccessLayer.Repository;
using System;
using System.Data;

namespace OutReachBusinessLayer
{
    public class ExcelToDB
    {
        OutreachMail outreachMail = new OutreachMail();
        public void FindTableName(string fileName, DataTable table)
        {
            try
            {
                switch (fileName)
                {
                    case "OutReach Event Information.xlsx":
                        InsertParticipatedUserDetails(table);
                        break;
                    case "Outreach Events Summary.xlsx":
                        InsertEventDetails(table);
                        break;
                    case "Volunteer_Enrollment Details_Not_Attend.xlsx":
                        InsertNotParticipatedUserDetails(table);
                        break;
                    case "Volunteer_Enrollment Details_Unregistered.xlsx":
                        InsertUnregisteredUserDetails(table);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InsertEventDetails(DataTable eventTable)
        {
            try
            {
                RoleRepository roleRepository = new RoleRepository();
                Role role = roleRepository.FindRoleId(ConstantValues.POC);

                foreach (DataRow row in eventTable.Rows)
                {
                    string eventID = row["Event ID"].ToString();
                    EventRepository eventRepository = new EventRepository();
                    Event Event = eventRepository.FindEvent(eventID);
                    if (Event == null)
                    {
                        Event = new Event();
                        Event.EventId = eventID;
                        if (!row.IsNull("Month"))
                        {
                            Event.Month = row["Month"].ToString();
                        }
                        if (!row.IsNull("Base Location"))
                        {
                            Event.Location = row["Base Location"].ToString();
                        }
                        if (!row.IsNull("Beneficiary Name"))
                        {
                            Event.BeneficaryName = row["Beneficiary Name"].ToString();
                        }
                        if (!row.IsNull("Venue Address"))
                        {
                            Event.Address = row["Venue Address"].ToString();
                        }
                        if (!row.IsNull("Council Name"))
                        {
                            Event.CouncilName = row["Council Name"].ToString();
                        }
                        if (!row.IsNull("Project"))
                        {
                            Event.Project = row["Project"].ToString();
                        }
                        if (!row.IsNull("Category"))
                        {
                            Event.Category = row["Category"].ToString();
                        }
                        if (!row.IsNull("Event Name"))
                        {
                            Event.EventName = row["Event Name"].ToString();
                        }
                        if (!row.IsNull("Event Description"))
                        {
                            Event.EventDescription = row["Event Description"].ToString();
                        }
                        if (!row.IsNull("Event Date (DD-MM-YY)"))
                        {
                            String[] date = row["Event Date (DD-MM-YY)"].ToString().Split(new[] { '-' });
                            int day = Convert.ToInt32(date[0]);
                            int month = Convert.ToInt32(date[1]);
                            int a = 20;
                            int year = int.Parse(a.ToString() + date[2].ToString());
                            DateTime eventDate = new DateTime(year, month, day);
                            Event.EventDate = eventDate;
                        }
                        if (!row.IsNull("Total no. of volunteers"))
                        {
                            Event.VolunteerCount = Convert.ToInt32(row["Total no. of volunteers"]);
                        }
                        if (!row.IsNull("Total Volunteer Hours"))
                        {
                            Event.VolunteerHours = Convert.ToInt32(row["Total Volunteer Hours"]);
                        }
                        if (!row.IsNull("Total Travel Hours"))
                        {
                            Event.TravelHours = Convert.ToInt32(row["Total Travel Hours"]);
                        }
                        if (!row.IsNull("Overall Volunteering Hours"))
                        {
                            Event.TotalVolunteeringHours = Convert.ToInt32(row["Overall Volunteering Hours"]);
                        }
                        if (!row.IsNull("Lives Impacted"))
                        {
                            Event.LivesImpacted = Convert.ToInt32(row["Lives Impacted"]);
                        }
                        if (!row.IsNull("Activity Type"))
                        {
                            Event.ActivityType = Convert.ToInt32(row["Activity Type"]);
                        }
                        if (!row.IsNull("Status"))
                        {
                            Event.Status = row["Status"].ToString();
                        }
                        eventRepository.AddEvent(Event);

                        // Add POC User

                        String[] pocID = null; String[] pocName = null;

                        if (role != null)
                        {
                            if (!row.IsNull("POC ID"))
                            {
                                pocID = row["POC ID"].ToString().Split(new[] { ';' });
                            }
                            if (!row.IsNull("POC Name"))
                            {
                                pocName = row["POC Name"].ToString().Split(new[] { ';' });
                            }
                            for (int i = 0; i < pocID.Length; i++)
                            {
                                User user = new User
                                {
                                    AssociateID = pocID[i],
                                    AssociateName = pocName[i],
                                    RoleID = role.RoleID,
                                    EventId = eventID
                                };
                                UserRepository userRepository = new UserRepository();
                                userRepository.AddUser(user);
                            }
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "ExcelToDB",
                    ActionrName = "InsertEventDetails",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
                throw ex;
            }
        }
        private void InsertParticipatedUserDetails(DataTable particaipateduserTable)
        {
            try
            {
                foreach (DataRow row in particaipateduserTable.Rows)
                {
                    RegisteredVolunteer registeredVolunteer = new RegisteredVolunteer();
                    registeredVolunteer.EventId = row["Event ID"].ToString();
                    if (!row.IsNull("Employee ID"))
                    {
                        registeredVolunteer.EmployeeID = row["Employee ID"].ToString();
                    }
                    if (!row.IsNull("Employee Name"))
                    {
                        registeredVolunteer.EmployeeName = row["Employee Name"].ToString();
                    }
                    if (!row.IsNull("Business Unit"))
                    {
                        registeredVolunteer.BusinessUnit = row["Business Unit"].ToString();
                    }
                    if (!row.IsNull("Volunteer Hours"))
                    {
                        registeredVolunteer.VolunteerHours = Convert.ToInt32(row["Volunteer Hours"]);
                    }
                    if (!row.IsNull("Travel Hours"))
                    {
                        registeredVolunteer.TravelHours = Convert.ToInt32(row["Travel Hours"]);
                    }
                    RegisteredVolunteerRepository registeredVolunteerRepository = new RegisteredVolunteerRepository();
                    registeredVolunteerRepository.AddRegisteredVolunteer(registeredVolunteer);
                }                
                outreachMail.SendMailToParticipatedUser(particaipateduserTable);
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "ExcelToDB",
                    ActionrName = "InsertParticipatedUserDetails",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
            }
        }
        private void InsertNotParticipatedUserDetails(DataTable notparticipateduserTable)
        {
            try
            {
                foreach (DataRow row in notparticipateduserTable.Rows)
                {
                    NotAttendedVolunteer notAttendedVolunteer = new NotAttendedVolunteer();
                    notAttendedVolunteer.EventId = row["Event ID"].ToString();
                    if (!row.IsNull("EmployeeID"))
                    {
                        notAttendedVolunteer.EmployeeID = row["EmployeeID"].ToString();
                    }
                    if (!row.IsNull("Event Name"))
                    {
                        notAttendedVolunteer.EventName = row["Event Name"].ToString();
                    }
                    if (!row.IsNull("Beneficiary Name"))
                    {
                        notAttendedVolunteer.BeneficaryName = row["Beneficiary Name"].ToString();
                    }
                    if (!row.IsNull("Base Location"))
                    {
                        notAttendedVolunteer.Location = row["Base Location"].ToString();
                    }
                    if (!row.IsNull("Event Date (DD-MM-YY)"))
                    {
                        String[] date = row["Event Date (DD-MM-YY)"].ToString().Split(new[] { '-' });
                        int day = Convert.ToInt32(date[0]);
                        int month = Convert.ToInt32(date[1]);
                        int a = 20;
                        int year = int.Parse(a.ToString() + date[2].ToString());
                        DateTime eventDate = new DateTime(year, month, day);
                        notAttendedVolunteer.EventDate = eventDate;
                    }
                    NotAttendedVolunteerRepository notAttendedVolunteerRepository = new NotAttendedVolunteerRepository();
                    notAttendedVolunteerRepository.AddNotAttendedVolunteer(notAttendedVolunteer);
                }
                outreachMail.SendMailToNotParticipatedUser(notparticipateduserTable);
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "ExcelToDB",
                    ActionrName = "InsertNotParticipatedUserDetails",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
            }
        }
        private void InsertUnregisteredUserDetails(DataTable unregistereduserTable)
        {
            try
            {
                foreach (DataRow row in unregistereduserTable.Rows)
                {
                    UnRegisteredVolunteer unRegisteredVolunteer = new UnRegisteredVolunteer();
                    unRegisteredVolunteer.EventId = row["Event ID"].ToString();
                    if (!row.IsNull("EmployeeID"))
                    {
                        unRegisteredVolunteer.EmployeeID = row["EmployeeID"].ToString();
                    }
                    if (!row.IsNull("Event Name"))
                    {
                        unRegisteredVolunteer.EventName = row["Event Name"].ToString();
                    }
                    if (!row.IsNull("Beneficiary Name"))
                    {
                        unRegisteredVolunteer.BeneficaryName = row["Beneficiary Name"].ToString();
                    }
                    if (!row.IsNull("Base Location"))
                    {
                        unRegisteredVolunteer.Location = row["Base Location"].ToString();
                    }
                    if (!row.IsNull("Event Date (DD-MM-YY)"))
                    {
                        String[] date = row["Event Date (DD-MM-YY)"].ToString().Split(new[] { '-' });
                        int day = Convert.ToInt32(date[0]);
                        int month = Convert.ToInt32(date[1]);
                        int a = 20;
                        int year = int.Parse(a.ToString() + date[2].ToString());
                        DateTime eventDate = new DateTime(year, month, day);
                        unRegisteredVolunteer.EventDate = eventDate;
                    }
                    UnRegisteredVolunteerRepository unRegisteredVolunteerRepository = new UnRegisteredVolunteerRepository();
                    unRegisteredVolunteerRepository.AddUnRegisteredVolunteer(unRegisteredVolunteer);
                }
                outreachMail.SendMailToUnregisteredUser(unregistereduserTable);
            }
            catch (Exception ex)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ControllerName = "ExcelToDB",
                    ActionrName = "InsertUnregisteredUserDetails",
                    ExceptionMessage = ex.Message,
                    ExceptionStackTrace = ex.StackTrace,
                    LogDateTime = DateTime.Now
                };
                ExceptionRepository exceptionRepository = new ExceptionRepository();
                exceptionRepository.AddException(logger);
            }
        }        
    }
}
