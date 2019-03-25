using System.ComponentModel.DataAnnotations;

namespace OutReachDTO.DTO
{
    public class NotAttendedVolunteerFeedbackDTO
    {
        public int Id { get; set; }

        [Required]
        public string EventId { get; set; }

        [Required]
        [StringLength(10)]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(50)]
        public string FeedbackText { get; set; }

        public string EventName { get; set; }

        public string EventDate { get; set; }
    }
}
