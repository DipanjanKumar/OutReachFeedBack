using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class RegisteredVolunteerFeedback
    {
        [Key]
        [ScaffoldColumn(false)]
        public int FeedbackId { get; set; }

        [StringLength(10)]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [ForeignKey("Event")]
        public string EventId { get; set; }
        public virtual Event Event { get; set; }

        [ForeignKey("FeedbackQuestion")]
        public int QuestionNumber { get; set; }
        public virtual FeedbackQuestion FeedbackQuestion { get; set; }

        [StringLength(500)]
        [Display(Name = "Feedback Text")]
        public string FeedbackText { get; set; }
    }
}
