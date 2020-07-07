using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using FMS_Web_Api.DAL;
using FMS_Web_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_Web_Api.Repository
{
    public interface IEventRepository
    {
        public Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int EventId);
        public Task<List<Event>> GetAll();

        public Task<Event> Get(int id);
        public Task<Event> Add(Event entity);
        public Task<Event> Delete(int id);
        public Task<Event> Update(Event entity);
        public Task<bool> DataFeed();
    }
    public class EventRepository : IEventRepository //: EfCoreRepository<Event, ApplicationDbContext>
    {
        private readonly ApplicationDbContext _context;
        private readonly EventParticipatedUsersRepository _eventParticipatedUserRepository;
        private readonly EventNotParticipatedUsersRepository _eventNotParticipatedUserRepository;
        private readonly EventUnregisteredUsersRepository _eventUnregisteredUserRepository;
        public EventRepository(ApplicationDbContext context, EventParticipatedUsersRepository eventParticipatedUserRepository, EventNotParticipatedUsersRepository eventNotParticipatedUsersRepository, EventUnregisteredUsersRepository eventUnregisteredUsersRepository) //: base(context)
        {
            _context = context;
            _eventParticipatedUserRepository = eventParticipatedUserRepository;
            _eventNotParticipatedUserRepository = eventNotParticipatedUsersRepository;
            _eventUnregisteredUserRepository = eventUnregisteredUsersRepository;
        }
        public async Task<Event> Add(Event entity)
        {
            _context.Set<Event>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event> Delete(int id)
        {
            var entity = await _context.Set<Event>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<Event>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Event> Get(int id)
        {
            return await _context.Set<Event>().FindAsync(id);
        }

        public async Task<List<Event>> GetAll()
        {
            return await _context.Set<Event>().ToListAsync();
        }

        public async Task<Event> Update(Event entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<IEnumerable<EventPocDetail>> GetEventPocDetails(int EventId)
        {
            return await _context.EventPocDetails.Where(x => x.EventId == EventId).ToListAsync();
        }
        public async Task<bool> DataFeed()
        {
            
            try
            {
                string summaryPath = System.IO.Directory.GetCurrentDirectory() + "\\DataFeed\\Summary.xlsx";
                var summaryWB = new XLWorkbook(summaryPath);
                if (summaryWB != null)
                {
                    await LoadSummaryData(summaryWB);
                }

                string participatedUsersPath = System.IO.Directory.GetCurrentDirectory() + "\\DataFeed\\ParticipatedUsers.xlsx";
                var participatedUsersWB = new XLWorkbook(participatedUsersPath);
                if (participatedUsersWB != null)
                {
                    await LoadParticipatedUsersData(participatedUsersWB);
                }

                string notParticipatedUsersPath = System.IO.Directory.GetCurrentDirectory() + "\\DataFeed\\NotParticipatedUsers.xlsx";
                var notParticipatedUsersWB = new XLWorkbook(notParticipatedUsersPath);
                if (notParticipatedUsersWB != null)
                {
                    await LoadNotParticipatedUsersData(notParticipatedUsersWB);
                }

                string unregisteredUsersPath = System.IO.Directory.GetCurrentDirectory() + "\\DataFeed\\UnregisteredUsers.xlsx";
                var unregisteredUsersWB = new XLWorkbook(unregisteredUsersPath);
                if (unregisteredUsersWB != null)
                {
                    await LoadUnregisteredUsersData(unregisteredUsersWB);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
        private async Task<bool> LoadSummaryData(XLWorkbook wb)
        {
            Event evnt = new Event();
            var ws1 = wb.Worksheet(1);
            List<Event> events = await _context.Set<Event>().ToListAsync();
            foreach (var row in ws1.Rows())
            {
                try
                {
                    if(row.Cell(1).Value.ToString() == "Event ID")
                    {
                        continue;
                    }
                    if(row.Cell(1).Value.ToString() == "")
                    {
                        break;
                    }
                    evnt.EventId = row.Cell(1).Value.ToString();
                    evnt.Month = row.Cell(2).Value.ToString();
                    evnt.BaseLocation = row.Cell(3).Value.ToString();
                    evnt.BeneficiaryName = row.Cell(4).Value.ToString();
                    evnt.VenueAddress = row.Cell(5).Value.ToString();
                    evnt.CouncilName = row.Cell(6).Value.ToString();
                    evnt.Project = row.Cell(7).Value.ToString();
                    evnt.Category = row.Cell(8).Value.ToString();
                    evnt.EventName = row.Cell(9).Value.ToString();
                    evnt.EventDescription = row.Cell(10).Value.ToString();
                    evnt.EventDate = Convert.ToDateTime(row.Cell(11).Value.ToString());
                    evnt.TotalNoOfVolunteers = Convert.ToInt16(row.Cell(12).Value.ToString());
                    evnt.TotalVolunteerHours = Convert.ToInt16(row.Cell(13).Value.ToString());
                    evnt.TotalTravelHours = Convert.ToInt16(row.Cell(14).Value.ToString());
                    evnt.OverallVolunteeringHours = Convert.ToInt16(row.Cell(15).Value.ToString());
                    evnt.LivesImpacted = Convert.ToInt16(row.Cell(16).Value.ToString());
                    evnt.ActivityType = row.Cell(17).Value.ToString();
                    evnt.Status = row.Cell(18).Value.ToString();

                    Event singleEvnt = events.Where(x => x.EventId == evnt.EventId).FirstOrDefault();
                    if (singleEvnt != null)
                    {
                        singleEvnt.EventId = evnt.EventId;
                        singleEvnt.Month = evnt.Month;
                        singleEvnt.BaseLocation = evnt.BaseLocation;
                        singleEvnt.BeneficiaryName = evnt.BeneficiaryName;
                        singleEvnt.VenueAddress = evnt.VenueAddress;
                        singleEvnt.CouncilName = evnt.CouncilName;
                        singleEvnt.Project = evnt.Project;
                        singleEvnt.Category = evnt.Category;
                        singleEvnt.EventName = evnt.EventName;
                        singleEvnt.EventDescription = evnt.EventDescription;
                        singleEvnt.EventDate = evnt.EventDate;
                        singleEvnt.TotalNoOfVolunteers = evnt.TotalNoOfVolunteers;
                        singleEvnt.TotalVolunteerHours = evnt.TotalVolunteerHours;
                        singleEvnt.TotalTravelHours = evnt.TotalTravelHours;
                        singleEvnt.OverallVolunteeringHours = evnt.OverallVolunteeringHours;
                        singleEvnt.LivesImpacted = evnt.LivesImpacted;
                        singleEvnt.ActivityType = evnt.ActivityType;
                        singleEvnt.Status = evnt.Status;                        
                        _context.Entry(singleEvnt).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        evnt.Id = 0;
                        _context.Set<Event>().Add(evnt);
                        await _context.SaveChangesAsync();
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }
            

            return true;
        }
        private async Task<bool> LoadParticipatedUsersData(XLWorkbook wb)
        {
            EventParticipatedUser participatedUser = new EventParticipatedUser();
            var ws1 = wb.Worksheet(1);
            List<EventParticipatedUser> participatedUsers = await _eventParticipatedUserRepository.GetAll();
            foreach (var row in ws1.Rows())
            {
                try
                {
                    if (row.Cell(1).Value.ToString() == "Event ID")
                    {
                        continue;
                    }
                    if (row.Cell(1).Value.ToString() == "")
                    {
                        break;
                    }
                    participatedUser.EventId = row.Cell(1).Value.ToString();
                    participatedUser.Email = row.Cell(2).Value.ToString();
                    
                    EventParticipatedUser singleUser = participatedUsers.Where(x => x.EventId == participatedUser.EventId && x.Email == participatedUser.Email).FirstOrDefault();
                    if (singleUser == null)
                    {
                        participatedUser.Id = 0;
                        _context.Set<EventParticipatedUser>().Add(participatedUser);
                        await _context.SaveChangesAsync();
                    }                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return true;
        }
        private async Task<bool> LoadNotParticipatedUsersData(XLWorkbook wb)
        {
            EventNotParticipatedUser notParticipatedUser = new EventNotParticipatedUser();
            var ws1 = wb.Worksheet(1);
            List<EventNotParticipatedUser> notparticipatedUsers = await _eventNotParticipatedUserRepository.GetAll();
            foreach (var row in ws1.Rows())
            {
                try
                {
                    if (row.Cell(1).Value.ToString() == "Event ID")
                    {
                        continue;
                    }
                    if (row.Cell(1).Value.ToString() == "")
                    {
                        break;
                    }
                    notParticipatedUser.EventId = row.Cell(1).Value.ToString();
                    notParticipatedUser.Email = row.Cell(2).Value.ToString();

                    EventNotParticipatedUser singleUser = notparticipatedUsers.Where(x => x.EventId == notParticipatedUser.EventId && x.Email == notParticipatedUser.Email).FirstOrDefault();
                    if (singleUser == null)
                    {
                        notParticipatedUser.Id = 0;
                        _context.Set<EventNotParticipatedUser>().Add(notParticipatedUser);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return true;
        }

        private async Task<bool> LoadUnregisteredUsersData(XLWorkbook wb)
        {
            EventUnregisteredUser unregisteredUser = new EventUnregisteredUser();
            var ws1 = wb.Worksheet(1);
            List<EventUnregisteredUser> unregisteredUsers = await _eventUnregisteredUserRepository.GetAll();
            foreach (var row in ws1.Rows())
            {
                try
                {
                    if (row.Cell(1).Value.ToString() == "Event ID")
                    {
                        continue;
                    }
                    if (row.Cell(1).Value.ToString() == "")
                    {
                        break;
                    }
                    unregisteredUser.EventId = row.Cell(1).Value.ToString();
                    unregisteredUser.Email = row.Cell(2).Value.ToString();

                    EventUnregisteredUser singleUser = unregisteredUsers.Where(x => x.EventId == unregisteredUser.EventId && x.Email == unregisteredUser.Email).FirstOrDefault();
                    if (singleUser == null)
                    {
                        unregisteredUser.Id = 0;
                        _context.Set<EventUnregisteredUser>().Add(unregisteredUser);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return true;
        }
    }
}
