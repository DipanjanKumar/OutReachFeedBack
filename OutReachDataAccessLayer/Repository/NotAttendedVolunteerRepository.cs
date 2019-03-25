using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;

namespace OutReachDataAccessLayer.Repository
{
    public class NotAttendedVolunteerRepository
    {
        private readonly IGenericRepository<NotAttendedVolunteer> INotAttendedVolunteerRepository = null;
        public NotAttendedVolunteerRepository()
        {
            INotAttendedVolunteerRepository = new GenericRepository<NotAttendedVolunteer>();
        }
        public void AddNotAttendedVolunteer(NotAttendedVolunteer notAttendedVolunteer)
        {
            INotAttendedVolunteerRepository.Insert(notAttendedVolunteer);
            INotAttendedVolunteerRepository.Save();
        }
    }
}
