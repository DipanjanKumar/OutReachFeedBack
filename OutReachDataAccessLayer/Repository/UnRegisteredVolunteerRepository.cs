using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;

namespace OutReachDataAccessLayer.Repository
{
    public class UnRegisteredVolunteerRepository
    {
        private readonly IGenericRepository<UnRegisteredVolunteer> IUnRegisteredVolunteerRepository = null;
        public UnRegisteredVolunteerRepository()
        {
            IUnRegisteredVolunteerRepository = new GenericRepository<UnRegisteredVolunteer>();
        }
        public void AddUnRegisteredVolunteer(UnRegisteredVolunteer unRegisteredVolunteer)
        {
            IUnRegisteredVolunteerRepository.Insert(unRegisteredVolunteer);
            IUnRegisteredVolunteerRepository.Save();
        }
    }
}
