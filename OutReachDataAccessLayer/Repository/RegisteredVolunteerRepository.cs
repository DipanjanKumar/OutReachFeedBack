using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class RegisteredVolunteerRepository
    {
        private readonly IGenericRepository<RegisteredVolunteer> IRegisteredVolunteerRepository = null;
        public RegisteredVolunteerRepository()
        {
            IRegisteredVolunteerRepository = new GenericRepository<RegisteredVolunteer>();
        }
        public List<RegisteredVolunteer> GetRegisteredVolunteerList()
        {
            return IRegisteredVolunteerRepository.SelectAll().ToList();
        }
        public RegisteredVolunteer FindRegisteredVolunteer(RegisteredVolunteer volunteer)
        {
            return IRegisteredVolunteerRepository.SelectByID(volunteer);
        }
        public void AddRegisteredVolunteer(RegisteredVolunteer volunteer)
        {
            IRegisteredVolunteerRepository.Insert(volunteer);
            IRegisteredVolunteerRepository.Save();
        }
        public RegisteredVolunteer GetRegisteredVolunteerByAssociateID(string AssociateID)
        {
            return IRegisteredVolunteerRepository.SelectAll().Where(rl => rl.EmployeeID.Equals(AssociateID)).FirstOrDefault();
        }
    }
}
