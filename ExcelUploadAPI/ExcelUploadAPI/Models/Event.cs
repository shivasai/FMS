using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExcelUploadAPI.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string EventId { get; set; }
        public string Month { get; set; }
        public string BaseLocation { get; set; }

        public string BeneficiaryName { get; set; }
        public string VenueAddress { get; set; }
        public string CouncilName { get; set; }
        public string Project { get; set; }
        public string Category { get; set; }

        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public int TotalNoOfVolunteers { get; set; }
        public double TotalVolunteerHours { get; set; }

        public double TotalTravelHours { get; set; }
        public double OverallVolunteeringHours { get; set; }
        public int LivesImpacted { get; set; }

        public string ActivityType { get; set; }
        public string Status { get; set; }
    }
}