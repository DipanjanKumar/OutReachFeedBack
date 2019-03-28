using NUnit.Framework;
using OutReachDataAccessLayer.Repository;

namespace OutReachDataAccessLayer.Tests
{
    [TestFixture]
    class TestEvent
    {
        EventRepository eventRepository = null;
        public TestEvent()
        {
            eventRepository = new EventRepository();
        }
        [Test]
        public void FindEvent()
        {
            Assert.AreEqual(eventRepository.FindEvent("EVNT00047261").Location, "Singapore");
        }
    }
}
