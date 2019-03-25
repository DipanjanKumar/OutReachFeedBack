using System;
using System.ComponentModel.DataAnnotations;

namespace OutReachFeedBack.Models
{
    public class EventDTO
    {
        [Display(Name = "Event ID")]
        public string EventId { get; set; }

        public string Month { get; set; }

        public string Location { get; set; }

        [Display(Name = "Beneficary Name")]
        public string BeneficaryName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Council Name")]
        public string CouncilName { get; set; }

        public string Project { get; set; }

        public string Category { get; set; }

        [Display(Name = "Event Name")]
        public string EventName { get; set; }

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

        public string Status { get; set; }
    }
}