using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    
    public class EventRepository : EfCoreRepository<Event, ApplicationDbContext>
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int EventId)
        {
            return await _context.EventPocDetails.Where(x => x.EventId == EventId).ToListAsync();
        }
    }
}
