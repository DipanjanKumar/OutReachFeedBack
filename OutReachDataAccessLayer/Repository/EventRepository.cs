using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace OutReachDataAccessLayer.Repository
{
    public class EventRepository
    {
        private readonly IGenericRepository<Event> IEventRepository = null;
        public EventRepository()
        {
            IEventRepository = new GenericRepository<Event>();
        }
        public List<Event> GetEventList()
        {
            return IEventRepository.SelectAll().ToList();
        }
        public Event FindEvent(string eventID)
        {
            return IEventRepository.SelectByID(eventID);
        }
        public void AddEvent(Event evt)
        {
            IEventRepository.Insert(evt);
            IEventRepository.Save();
        }
    }
}
