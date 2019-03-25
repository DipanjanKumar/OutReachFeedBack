using System.ComponentModel.DataAnnotations;

namespace OutReachDTO.DTO
{
    public class UnRegisteredVolunteerFeedbackDTO
    {
        public int Id { get; set; }
        public string EventId { get; set; }

        [StringLength(10)]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [StringLength(50)]
        [Display(Name = "Feedback Text")]
        public string FeedbackText { get; set; }
    }
}
