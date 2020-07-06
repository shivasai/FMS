using FMS_Web_Api.Controllers;
using FMS_Web_Api.Models;
using FMS_Web_Api.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Web_Api.Tests
{
    
    class FeedbackTests
    {
        private FeedbackController feedbackController;
        private readonly Mock<IFeedbackRepository> _feedbackRepository = new Mock<IFeedbackRepository>();
        [SetUp]
        public void Setup()
        {
            feedbackController = new FeedbackController(_feedbackRepository.Object);
        }
        [Test]
        public async Task GetFeedbackQnsAsync_ShouldReturnFeedbackQns_FeedbackQnExists()
        {           
            List<FeedbackVM> feedbackVMs = new List<FeedbackVM>();
            FeedbackVM feedback = new FeedbackVM()
            {
                Id = 1,
                Question = "How is the event?",
                QuestionTye = "MultipleAnswer"
            };
            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetFeedbackQuestions()).ReturnsAsync(feedbackVMs);

            IEnumerable<FeedbackVM> result = await feedbackController.Get();
            Assert.IsNotEmpty(result);
           
        }
        [Test]
        public async Task PostFbQuestionsByPartIdAsync_ShouldPostFbQuestionsByPartId()
        {
            PostFeedback postFeedback = new PostFeedback();
            postFeedback.ParticipantType = "Participated";
            List<FeedbackVM> feedbackVMs = new List<FeedbackVM>();
            FeedbackVM feedback = new FeedbackVM()
            {
                Id = 1,
                Question = "How is the event?",
                QuestionTye = "MultipleAnswer"
            };
            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetFeedbackQuestionsByParticipantType(postFeedback)).ReturnsAsync(feedbackVMs);
            IEnumerable<FeedbackVM> result = await feedbackController.PostFeedbackQuestionsByParticipantType(postFeedback);
            Assert.IsNotEmpty(result);
        }
        [Test]
        public async Task GetFeedbackQnsByIdAsync_ShouldReturnGetFeedbackQns_FeedbackQnExists()
        {
            int id = 1;
            FeedbackVM feedback = new FeedbackVM()
            {
                Id = 1,
                Question = "How is the event?",
                QuestionTye = "MultipleAnswer"
            };
            _feedbackRepository.Setup(x => x.GetFeedbackQuestionById(id)).ReturnsAsync(feedback);
            FeedbackVM result = await feedbackController.Get(id);
            Assert.AreEqual(feedback.Id, result.Id);
            Assert.AreEqual(feedback.Question, result.Question);
            Assert.AreEqual(feedback.QuestionTye, result.QuestionTye);
        }
        [Test]
        public async Task SaveFbQstnAndAnsAsync_ShouldSaveFbQstnAndAns()
        {
            PostFeedback postFeedback = new PostFeedback();
            postFeedback.ParticipantType = "Participated";
            PostFeedback resultFeedback = new PostFeedback();
            resultFeedback.ParticipantType = "Participated";
            resultFeedback.QuestionTye = "MultipleAnswer";

            _feedbackRepository.Setup(x => x.SaveFeedbackQuestionAndAnswers(postFeedback)).ReturnsAsync(resultFeedback);
            PostFeedback result = await feedbackController.Post(postFeedback);
            Assert.AreEqual(result.QuestionTye, "MultipleAnswer");           
        }
        [Test]
        public async Task GetParticipantFbAsync_ShouldReturnParticipantFb_ParticipantFbExists()
        {
            int id = 1;
            List<ParticipantFeedbackVM> feedbackVMs = new List<ParticipantFeedbackVM>();
            ParticipantFeedbackVM feedback = new ParticipantFeedbackVM();
            List<string> fbNames = new List<string>();
            fbNames.Add("Good Event");
            fbNames.Add("Excellent");
            feedback.Feedback = fbNames;


            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetParticipantFeedbacksForEvent(id)).ReturnsAsync(feedbackVMs);

            IEnumerable<ParticipantFeedbackVM> result = await feedbackController.GetParticipantFeedbacks(id);
            Assert.IsNotEmpty(result);

        }

        [Test]
        public async Task GetNotParticipantFbAsync_ShouldReturnNotParticipantFb_NotParticipantFbExists()
        {
            int id = 1;
            List<ParticipantFeedbackVM> feedbackVMs = new List<ParticipantFeedbackVM>();
            ParticipantFeedbackVM feedback = new ParticipantFeedbackVM();
            List<string> fbNames = new List<string>();
            fbNames.Add("Incorrectly registered");
            fbNames.Add("Unexpected");
            feedback.Feedback = fbNames;


            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetNotParticipatedFeedbacksForEvent(id)).ReturnsAsync(feedbackVMs);

            IEnumerable<ParticipantFeedbackVM> result = await feedbackController.GetNotParticipatedFeedbacks(id);
            Assert.IsNotEmpty(result);

        }
        [Test]
        public async Task GetunregisteredFbAsync_ShouldReturnunregisteredFb_unregisteredFbExists()
        {
            int id = 1;
            List<ParticipantFeedbackVM> feedbackVMs = new List<ParticipantFeedbackVM>();
            ParticipantFeedbackVM feedback = new ParticipantFeedbackVM();
            List<string> fbNames = new List<string>();
            fbNames.Add("Incorrectly registered");            
            feedback.Feedback = fbNames;


            feedbackVMs.Add(feedback);
            _feedbackRepository.Setup(x => x.GetUnregisteredFeedbacksForEvent(id)).ReturnsAsync(feedbackVMs);

            IEnumerable<ParticipantFeedbackVM> result = await feedbackController.GetUnregisteredFeedbacks(id);
            Assert.IsNotEmpty(result);

        }
    }
}
