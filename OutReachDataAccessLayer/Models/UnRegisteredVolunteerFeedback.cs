using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class UnRegisteredVolunteerFeedback
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ForeignKey("Event")]
        public string EventId { get; set; }
        public virtual Event Event { get; set; }

        [StringLength(10)]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [StringLength(50)]
        [Display(Name = "Feedback Text")]
        public string FeedbackText { get; set; }
    }
}
