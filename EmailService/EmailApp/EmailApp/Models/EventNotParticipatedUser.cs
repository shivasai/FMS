using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.Models
{
    public class EventNotParticipatedUser
    {
        [Key]
        public int Id { get; set; }
        public string EventId { get; set; }

        public string Email { get; set; }
    }
}
