using EmailApp.Data;
using EmailApp.Models;
using EmailService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.Repository
{
    public interface IEmailRepository
    {
        public Task<bool> GenerateEmails(Event events);
    }
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IEmailSender _emailSender;
        public EmailRepository(ApplicationDBContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public async Task<bool> GenerateEmails(Event events)
        {
            string hostUrl = "http://localhost:4200";
            var allEvents = await _context.Events.ToListAsync();
            if (events.EventId != string.Empty)
            {
                Event evnt = new Event();
                string evntId = events.EventId;
                evnt = allEvents.Where(x => x.EventId == evntId).FirstOrDefault();

                await GenerateEmail(evnt, hostUrl);
            }
            else
            {

                foreach (var singleEvent in allEvents)
                {
                    await GenerateEmail(singleEvent, hostUrl);
                }
            }
            return true;
        }
        private async Task<bool> GenerateEmail(Event evnt, string hostUrl)
        {
            string emailMessage = ""; string emailSubject = "";
            var eventParticipatedUser = await _context.EventParticipatedUsers.Where(x => x.EventId == evnt.EventId).ToListAsync();
            emailSubject = "Feedback for Event : " + evnt.EventName;
            //var eventDetails = await _context.Events.FirstOrDefault(x => x.EventId == eventId);
            if (eventParticipatedUser.Count() > 0)
            {
                List<string> ParticipatedAddresses = new List<string>();
                foreach (var participatedUser in eventParticipatedUser)
                {
                    ParticipatedAddresses.Add(participatedUser.Email);
                }

                emailMessage = "<h3>Please click the below link to share your feedback<h/3><br><a href='" + hostUrl + "/participantfeedback?eventid=" + evnt.EventId + "&type=participated'>Share feedback!</a>";
                await SendMails(ParticipatedAddresses, emailSubject, emailMessage);
            }

            var eventNotParticipatedUser = await _context.EventNotParticipatedUsers.Where(x => x.EventId == evnt.EventId).ToListAsync();
            if (eventNotParticipatedUser.Count() > 0)
            {
                List<string> NotParticipatedAddresses = new List<string>();
                foreach (var notparticipatedUser in eventNotParticipatedUser)
                {
                    NotParticipatedAddresses.Add(notparticipatedUser.Email);
                }
                emailMessage = "<h3>Please click the below link to share your feedback<h/3><br><a href='" + hostUrl + "/participantfeedback?eventid=" + evnt.EventId + "&type=notparticipated'>Share feedback!</a>";
                await SendMails(NotParticipatedAddresses, emailSubject, emailMessage);
            }

            var eventunregisteredUser = await _context.EventUnregisteredUsers.Where(x => x.EventId == evnt.EventId).ToListAsync();
            if (eventunregisteredUser.Count() > 0)
            {
                List<string> UnregisteredAddresses = new List<string>();
                foreach (var unregisteredUser in eventunregisteredUser)
                {
                    UnregisteredAddresses.Add(unregisteredUser.Email);
                }
                emailMessage = "<h3>Please click the below link to share your feedback<h/3><br><a href='" + hostUrl + "/participantfeedback?eventid=" + evnt.EventId + "&type=unregistered'>Share feedback!</a>";
                await SendMails(UnregisteredAddresses, emailSubject, emailMessage);
            }


            return true;
        }
        private async Task<bool> SendMails(List<string> ToAddresses, string Subject, string Body)
        {
            var message = new Message(ToAddresses, Subject, Body, null);
            await _emailSender.SendEmailAsync(message);
            return true;

        }
    }
}
    
