using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class UnRegisteredVolunteerFeedbackRepository
    {
        private readonly IGenericRepository<UnRegisteredVolunteerFeedback> unregistervolunteerfeedbackRepository = null;
        public UnRegisteredVolunteerFeedbackRepository()
        {
            unregistervolunteerfeedbackRepository = new GenericRepository<UnRegisteredVolunteerFeedback>();
        }
        public UnRegisteredVolunteerFeedback GetUnRegisteredVolunteerFeedback(string eventId, string employeeId)
        {
            return unregistervolunteerfeedbackRepository.SelectAll().Where(reg => reg.EventId.Equals(eventId) && reg.EmployeeID.Equals(employeeId)).FirstOrDefault();
        }
        public List<UnRegisteredVolunteerFeedback> GetUnRegisteredVolunteerFeedbackList(string eventId)
        {
            return unregistervolunteerfeedbackRepository.SelectAll().Where(reg => reg.EventId.Equals(eventId)).ToList();
        }
        public void SaveUnRegisteredVolunteerFeedback(UnRegisteredVolunteerFeedback unRegisteredVolunteerFeedback)
        {
            unregistervolunteerfeedbackRepository.Insert(unRegisteredVolunteerFeedback);
            unregistervolunteerfeedbackRepository.Save();
        }
    }
}
