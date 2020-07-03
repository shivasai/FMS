using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmailApp.Data;
using EmailApp.Models;
using EmailService;
using System.Security.Cryptography.X509Certificates;
using EmailApp.Repository;

namespace EmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailServiceController : ControllerBase
    {       
        private readonly IEmailRepository _emailRepository;
        public MailServiceController(IEmailRepository emailRepository)
        {            
            _emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Event events)
        {            
            await _emailRepository.GenerateEmails(events);
            
            return Ok();
        }        
    }
}
