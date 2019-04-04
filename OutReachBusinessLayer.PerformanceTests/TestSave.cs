using NBench;
using OutReachBusinessLayer.FeedBack;
using OutReachDTO.DTO;

namespace OutReachBusinessLayer.PerformanceTests
{
    public class TestSave
    {
        [PerfBenchmark(Description = "Performance test method to determine memory usage for this method.",
        NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated,MustBe.GreaterThan,ByteConstants.ThirtyTwoKb)]
        public void TestNotParticipatedSave()
        {
            NotParticipated notParticipated = new NotParticipated();
            NotAttendedVolunteerFeedbackDTO notAttendedVolunteerFeedbackDTO = new NotAttendedVolunteerFeedbackDTO();
            notAttendedVolunteerFeedbackDTO.EventId = "EVNT00047261";
            notAttendedVolunteerFeedbackDTO.EmployeeID = "711876";
            notAttendedVolunteerFeedbackDTO.FeedbackText = "Test FeedBack";
            notParticipated.SaveNotAttendedVolunteerFeedback(notAttendedVolunteerFeedbackDTO);
        }
    }
}
