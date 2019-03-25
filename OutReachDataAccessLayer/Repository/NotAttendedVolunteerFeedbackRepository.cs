using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class NotAttendedVolunteerFeedbackRepository
    {
        private readonly IGenericRepository<NotAttendedVolunteerFeedback> notattendedvolunteerfeedbackRepository = null;
        public NotAttendedVolunteerFeedbackRepository()
        {
            notattendedvolunteerfeedbackRepository = new GenericRepository<NotAttendedVolunteerFeedback>();
        }
        public NotAttendedVolunteerFeedback GetNotAttendedVolunteerFeedback(string eventId, string employeeId)
        {
            return notattendedvolunteerfeedbackRepository.SelectAll().Where(reg => reg.EventId.Equals(eventId) && reg.EmployeeID.Equals(employeeId)).FirstOrDefault();
        }
        public List<NotAttendedVolunteerFeedback> GetNotAttendedVolunteerFeedbackList(string eventId)
        {
            return notattendedvolunteerfeedbackRepository.SelectAll().Where(reg => reg.EventId.Equals(eventId)).ToList();
        }
        public void SaveNotAttendedVolunteerFeedback(NotAttendedVolunteerFeedback notAttendedVolunteerFeedback)
        {
            notattendedvolunteerfeedbackRepository.Insert(notAttendedVolunteerFeedback);
            notattendedvolunteerfeedbackRepository.Save();
        }
    }
}
