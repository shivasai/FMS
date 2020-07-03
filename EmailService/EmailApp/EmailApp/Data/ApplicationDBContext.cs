using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmailApp.Models;

namespace EmailApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext (DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipatedUser> EventParticipatedUsers { get; set; }

        public DbSet<EventNotParticipatedUser> EventNotParticipatedUsers { get; set; }

        public DbSet<EventUnregisteredUser> EventUnregisteredUsers { get; set; }
    }
}
