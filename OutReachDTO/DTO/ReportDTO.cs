using System.Collections.Generic;

namespace OutReachDTO.DTO
{
    public class ReportDTO
    {
        public List<RegisteredReportDTO> RegisteredReportDTOs { get; set; }
        public List<NotAttendedReportDTO> NotAttendedReportDTOs { get; set; }
        public List<UnregisteredReportDTO> UnregisteredReportDTOs { get; set; }
    }
}