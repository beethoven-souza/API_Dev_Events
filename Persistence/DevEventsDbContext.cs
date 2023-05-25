using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDevEvents.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDevEvents.Persistence
{
    public class DevEventsDbContext : DbContext
    {
         public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {
        }
        
        public DbSet<DevEvents> DevEvents {get;set;}
        public DbSet<DevEventSpeaker> DevEventSpeakers {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevEvents>(e =>
            {
                e.HasKey(de => de.Id);
                e.Property(de => de.Title).IsRequired(false);
                e.Property(de => de.Description).HasMaxLength(200).HasColumnType("varchar(200)");
                e.Property(de => de.StartDate).HasColumnName("Start_Date");
                e.Property(de => de.EndDate).HasColumnName("End_Date");
                e.Property(de => de.EndDate).HasColumnName("End_Date");
                e.HasMany(de => de.Speakers).WithOne().HasForeignKey(s => s.DevEventId);
            });
            modelBuilder.Entity<DevEventSpeaker>(e => e.HasKey(de => de.Id));
        }
       
    }
}