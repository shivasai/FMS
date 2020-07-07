using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace FMS_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _repository;

        public EventsController(IEventRepository repository)
        {
            _repository = repository;
        }
        [AllowAnonymous]
        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _repository.GetAll();
            //return await _context.Events.ToListAsync();
        }
        [AllowAnonymous]
        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            //var @event = await _context.Events.FindAsync(id);
            var @event = await _repository.Get(id);
            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }
        // GET: api/Events
        [HttpGet()]
        [Route("EventPocDetails/{id}")]
        public async Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int id)
        {
            return await _repository.GetEventPocDetails(id);
            //return await _context.Events.ToListAsync();
        }       
        [HttpPut()]
        [Route("DataFeed")]
        public async Task<IActionResult> PutDataFeed()
        {
            bool res =  await _repository.DataFeed();
            if (res == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
