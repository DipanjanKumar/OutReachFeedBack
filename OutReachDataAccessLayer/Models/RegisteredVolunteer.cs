using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class RegisteredVolunteer
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [StringLength(10)]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [StringLength(50)]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [StringLength(50)]
        [Display(Name = "Business Unit")]
        public string BusinessUnit { get; set; }

        [Display(Name = "Volunteers Hours")]
        public int? VolunteerHours { get; set; }

        [Display(Name = "Travel Hours")]
        public int? TravelHours { get; set; }

        [ForeignKey("Event")]
        public string EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
