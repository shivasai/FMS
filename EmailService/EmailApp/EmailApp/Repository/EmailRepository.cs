using EmailApp.Data;
using EmailApp.Models;
using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.Diagnostics.Tracing;

namespace EmailApp.Repository
{
    public interface IEmailRepository
    {
        public Task<bool> GenerateEmails(Event events);
        public Task<bool> SendReport(string email);
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
                    emailMessage = "<h3>Please click the below link to share your feedback<h/3><br><a href='" + hostUrl + "/participantfeedback?eventid=" + evnt.Id + "&type=participated&email=" + participatedUser.Email + "'>Share feedback!</a>";
                    await SendMails(ParticipatedAddresses, emailSubject, emailMessage);
                    ParticipatedAddresses.Clear();
                }
            }

            var eventNotParticipatedUser = await _context.EventNotParticipatedUsers.Where(x => x.EventId == evnt.EventId).ToListAsync();
            if (eventNotParticipatedUser.Count() > 0)
            {
                List<string> NotParticipatedAddresses = new List<string>();
                foreach (var notparticipatedUser in eventNotParticipatedUser)
                {
                    NotParticipatedAddresses.Add(notparticipatedUser.Email);
                    emailMessage = "<h3>Please click the below link to share your feedback<h/3><br><a href='" + hostUrl + "/participantfeedback?eventid=" + evnt.Id + "&type=notparticipated&email=" + notparticipatedUser.Email + "'>Share feedback!</a>";
                    await SendMails(NotParticipatedAddresses, emailSubject, emailMessage);
                    NotParticipatedAddresses.Clear();
                }

            }

            var eventunregisteredUser = await _context.EventUnregisteredUsers.Where(x => x.EventId == evnt.EventId).ToListAsync();
            if (eventunregisteredUser.Count() > 0)
            {
                List<string> UnregisteredAddresses = new List<string>();
                foreach (var unregisteredUser in eventunregisteredUser)
                {
                    UnregisteredAddresses.Add(unregisteredUser.Email);
                    emailMessage = "<h3>Please click the below link to share your feedback<h/3><br><a href='" + hostUrl + "/participantfeedback?eventid=" + evnt.Id + "&type=unregistered&email=" + unregisteredUser.Email + "'>Share feedback!</a>";
                    await SendMails(UnregisteredAddresses, emailSubject, emailMessage);
                    UnregisteredAddresses.Clear();
                }

            }


            return true;
        }
        private async Task<bool> SendMails(List<string> ToAddresses, string Subject, string Body)
        {
            var message = new Message(ToAddresses, Subject, Body, null);
            await _emailSender.SendEmailAsync(message);
            return true;

        }
        public async Task<bool> SendReport(string email)
        {
            var allEvents = await _context.Events.ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Events");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Event ID";
                worksheet.Cell(currentRow, 2).Value = "Month";
                worksheet.Cell(currentRow, 3).Value = "Base Location";
                worksheet.Cell(currentRow, 4).Value = "Council Name";
                worksheet.Cell(currentRow, 5).Value = "Beneficiary Name";
                worksheet.Cell(currentRow, 6).Value = "Event Name";
                worksheet.Cell(currentRow, 7).Value = "Event Date";
                worksheet.Cell(currentRow, 8).Value = "Status";
                worksheet.Cell(currentRow, 9).Value = "Venue Address";
                worksheet.Cell(currentRow, 10).Value = "Total Volunteers";
                worksheet.Cell(currentRow, 11).Value = "Total Volunteer Hours";
                worksheet.Cell(currentRow, 12).Value = "Total Travel Hours";
                foreach (var evnt in allEvents)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = evnt.EventId;
                    worksheet.Cell(currentRow, 2).Value = evnt.Month;
                    worksheet.Cell(currentRow, 3).Value = evnt.BaseLocation;
                    worksheet.Cell(currentRow, 4).Value = evnt.CouncilName;
                    worksheet.Cell(currentRow, 5).Value = evnt.BeneficiaryName;
                    worksheet.Cell(currentRow, 6).Value = evnt.EventName;
                    worksheet.Cell(currentRow, 7).Value = evnt.EventDate;
                    worksheet.Cell(currentRow, 8).Value = evnt.Status;
                    worksheet.Cell(currentRow, 9).Value = evnt.VenueAddress;
                    worksheet.Cell(currentRow, 10).Value = evnt.TotalNoOfVolunteers;
                    worksheet.Cell(currentRow, 11).Value = evnt.TotalVolunteerHours;
                    worksheet.Cell(currentRow, 12).Value = evnt.TotalTravelHours;
                }
                string path = System.IO.Directory.GetCurrentDirectory() + "\\Files\\Report.xlsx";
                workbook.SaveAs(path);
            }
            await _emailSender.SendReport(email);
            return true;
        }
    }
}

