using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class RegisteredVolunteerFeedbackRepository
    {
        private readonly IGenericRepository<RegisteredVolunteerFeedback> registervolunteerRepository = null;
        public RegisteredVolunteerFeedbackRepository()
        {
            registervolunteerRepository = new GenericRepository<RegisteredVolunteerFeedback>();
        }
        public List<RegisteredVolunteerFeedback> GetRegisteredFeeackList(string eventId,string employeeId)
        {
            return registervolunteerRepository.SelectAll().Where(reg => reg.EventId.Equals(eventId) && reg.EmployeeID.Equals(employeeId)).ToList();
        }
        public List<RegisteredVolunteerFeedback> GetRegisteredFeeackListByEvent(string eventId)
        {
            return registervolunteerRepository.SelectAll().Where(reg => reg.EventId.Equals(eventId)).ToList();
        }
            public void SaveRegisteredVolunteerFeedback(RegisteredVolunteerFeedback registeredVolunteerFeedback)
        {
            registervolunteerRepository.Insert(registeredVolunteerFeedback);
            registervolunteerRepository.Save();
        }
    }
}
