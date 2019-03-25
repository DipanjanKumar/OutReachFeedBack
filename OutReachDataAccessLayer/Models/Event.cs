using System;
using System.ComponentModel.DataAnnotations;

namespace OutReachDataAccessLayer.Models
{
    public class Event
    {
        [Key]
        [ScaffoldColumn(false)]
        [StringLength(20)]
        public string EventId { get; set; }

        [StringLength(10)]
        public string Month { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [StringLength(100)]
        [Display(Name = "Beneficary Name")]
        public string BeneficaryName { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(100)]
        [Display(Name = "Council Name")]
        public string CouncilName { get; set; }

        [StringLength(100)]
        public string Project { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(100)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [StringLength(500)]
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Total Volunteers")]
        public int? VolunteerCount { get; set; }

        [Display(Name = "Total Volunteers Hours")]
        public int? VolunteerHours { get; set; }

        [Display(Name = "Total Travel Hours")]
        public int? TravelHours { get; set; }

        [Display(Name = "Total Volunteering Hours")]
        public int? TotalVolunteeringHours { get; set; }

        [Display(Name = "Lives Impacted")]
        public int? LivesImpacted { get; set; }

        [Display(Name = "ActivityType")]
        public int? ActivityType { get; set; }

        [StringLength(10)]
        public string Status { get; set; }
    }
}
