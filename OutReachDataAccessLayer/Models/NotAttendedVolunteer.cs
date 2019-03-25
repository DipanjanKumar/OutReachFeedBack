using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutReachDataAccessLayer.Models
{
    public class NotAttendedVolunteer
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [StringLength(100)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [ForeignKey("Event")]
        public string EventId { get; set; }
        public virtual Event Event { get; set; }

        [StringLength(100)]
        [Display(Name = "Beneficary Name")]
        public string BeneficaryName { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }

        [StringLength(10)]
        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }
    }
}
