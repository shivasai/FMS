using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    public interface IFeedbackRepository
    {
        public Task<FeedbackVM> SaveFeedbackQuestionAndAnswers(FeedbackVM feedbackVM);
        public Task<IEnumerable<FeedbackVM>> GetFeedbackQuestions();
        public Task<IEnumerable<ParticipantFeedbackVM>> GetParticipantFeedbacksForEvent(int eventId);
        public Task<IEnumerable<ParticipantFeedbackVM>> GetNotParticipatedFeedbacksForEvent(int eventId);
        public Task<IEnumerable<ParticipantFeedbackVM>> GetUnregisteredFeedbacksForEvent(int eventId);
    }
    public class FeedbackRepository : IFeedbackRepository
    {
        public readonly FeedbackQuestionRepository _fbQuestionRepository;
        public readonly FeedbackOptionRepository _fbOptionRepository;
        public readonly ParticipantFeedbackRepository _participantFbRepository;
        public FeedbackRepository(FeedbackQuestionRepository feedbackQuestionRepository, FeedbackOptionRepository feedbackOptionRepository, ParticipantFeedbackRepository participantFeedbackRepository)
        {
            _fbQuestionRepository = feedbackQuestionRepository;
            _fbOptionRepository = feedbackOptionRepository;
            _participantFbRepository = participantFeedbackRepository;
        }

        public async Task<FeedbackVM> SaveFeedbackQuestionAndAnswers(FeedbackVM feedbackVM)
        {
            FeedbackQuestion feedbackQuestion = new FeedbackQuestion();
            feedbackQuestion.Question = feedbackVM.Question;
            feedbackQuestion.QuestionTye = feedbackVM.QuestionTye;
            feedbackQuestion.ParticipantType = feedbackVM.ParticipantType;

            feedbackQuestion = await _fbQuestionRepository.Add(feedbackQuestion);

            foreach (FeedbackOption fbOption in feedbackVM.FeedbackOptions)
            {
                fbOption.QuestionId = feedbackQuestion.Id;
                await _fbOptionRepository.Add(fbOption);
            }
            return feedbackVM;
        }


        public async Task<IEnumerable<FeedbackVM>> GetFeedbackQuestions()
        {
            List<FeedbackVM> allQuestions = new List<FeedbackVM>();

            var fbAllQuestions = await _fbQuestionRepository.GetAll();

            foreach (var fbQuestion in fbAllQuestions)
            {
                FeedbackVM feedbackVM = new FeedbackVM();
                feedbackVM.Id = fbQuestion.Id;
                feedbackVM.Question = fbQuestion.Question;
                feedbackVM.ParticipantType = fbQuestion.ParticipantType;
                feedbackVM.QuestionTye = fbQuestion.QuestionTye;

                var questionOptions = await _fbOptionRepository.GetAll();
                int optionCnt = questionOptions.Where(x => x.QuestionId == fbQuestion.Id).Count();
                feedbackVM.OptionsCount = optionCnt;

                allQuestions.Add(feedbackVM);
            }

            return allQuestions;
        }

        // Get Participant feedback
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetParticipantFeedbacksForEvent(int eventId)
        {
            return await GetFeedbacksForEvent(eventId, "Participated");
        }

        //Not participated users feedback
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetNotParticipatedFeedbacksForEvent(int eventId)
        {
            return await GetFeedbacksForEvent(eventId, "NotParticipated");
        }
        //Unregistered users feedback
        public async Task<IEnumerable<ParticipantFeedbackVM>> GetUnregisteredFeedbacksForEvent(int eventId)
        {
            return await GetFeedbacksForEvent(eventId, "Unregistered");
        }
        private async Task<IEnumerable<ParticipantFeedbackVM>> GetFeedbacksForEvent(int eventId, string ParticipantType)
        {
            List<ParticipantFeedbackVM> fbVM = new List<ParticipantFeedbackVM>();

            var allFeedbacks = await _participantFbRepository.GetAll();
            //List<string> participatedEmails = allFeedbacks.Where(x => x.EventId == eventId).Select(x=>x.Email).ToList();
            var distinctEmails = allFeedbacks.GroupBy(test => test.Email)
                        .Select(grp => grp.First());
            foreach (var email in distinctEmails)
            {
                ParticipantFeedbackVM pfbVM = new ParticipantFeedbackVM();
                pfbVM.Feedback = new List<string>();
                var fbs = allFeedbacks.Where(x => x.Email == email.Email && x.ParticipantType == ParticipantType).Select(x => x.Answer).ToList();
                foreach (var fb in fbs)
                {
                    // Answer answer = new Answer();
                    // answer.Ans = fb.ToString();
                    pfbVM.Feedback.Add(fb);
                }
                if (pfbVM.Feedback.Count > 0)
                {
                    fbVM.Add(pfbVM);
                }

            }
            return fbVM;
        }
    }
}
