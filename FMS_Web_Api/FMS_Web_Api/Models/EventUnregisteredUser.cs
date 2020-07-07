using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Models
{
    public class EventUnregisteredUser : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string EventId { get; set; }

        public string Email { get; set; }
    }
}
