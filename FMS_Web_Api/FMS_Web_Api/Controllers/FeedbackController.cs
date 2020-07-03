using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackRepository _repository;
        public FeedbackController(FeedbackRepository feedbackRepository)
        {
            _repository = feedbackRepository;
        }
        // GET: api/<FeedbackController>
        [HttpGet]
        public async Task<IEnumerable<FeedbackVM>> Get()
        {
            return await _repository.GetFeedbackQuestions();
        }

        [HttpGet]
        [Route("ParticipantFeedbacks/{id}")]
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetParticipantFeedbacks(int id)
        {
            return await _repository.GetParticipantFeedbacksForEvent(id);
        }
        [HttpGet]
        [Route("NotParticipatedFeedbacks/{id}")]
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetNotParticipatedFeedbacks(int id)
        {
            return await _repository.GetNotParticipatedFeedbacksForEvent(id);
        }
        [HttpGet]
        [Route("UnregisteredFeedbacks/{id}")]
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetUnregisteredFeedbacks(int id)
        {
            return await _repository.GetUnregisteredFeedbacksForEvent(id);
        }
    }
}
