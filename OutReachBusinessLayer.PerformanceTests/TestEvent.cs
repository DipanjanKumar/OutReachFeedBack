using NBench;
using OutReachBusinessLayer.Events;
using OutReachDTO.DTO;

namespace OutReachBusinessLayer.PerformanceTests
{
    public class TestEvent
    {
        OutReachEvent outReachEvent = null;
        public TestEvent()
        {
            outReachEvent = new OutReachEvent();
        }
        [PerfBenchmark(Description = "Performance test method to determine elapsed time for this method.",
        NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Measurement)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds =1000,MinTimeMilliseconds =900)]
        public void FindEventByID()
        {
            EventDTO eventDTO = outReachEvent.GetEventById("EVNT00047261");
        }
    }
}
