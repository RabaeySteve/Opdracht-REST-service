using FitnessManagement.BL.Models;
using FitnessManagement.EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF {
    public class FitnessManagementContext : DbContext {

        private string connectionString;

        

        public DbSet<MemberEF> members { get; set; }
        public DbSet<ProgramEF> Programs { get; set; }
        public DbSet<RunningSessionEF> runningsession_main { get; set; }
        public DbSet<CyclingSessionEF> cyclingsession { get; set; }
        public DbSet<EquipmentEF> equipment { get; set; }
        public DbSet<ReservationEF> reservation { get; set; }
        public DbSet<TimeSlotEF> time_slot { get; set; }
        public DbSet<RunningSessionDetailsEF> runningsession_detail { get; set; }

        public FitnessManagementContext(string connectionString) {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ProgramEF>()
               .HasMany<MemberEF>() 
               .WithMany()          
               .UsingEntity<Dictionary<string, object>>("programmembers", 
           j => j.HasOne<MemberEF>() 
                 .WithMany()
                 .HasForeignKey("MemberId")
                 .OnDelete(DeleteBehavior.Cascade), 
           j => j.HasOne<ProgramEF>() 
                 .WithMany()
                 .HasForeignKey("ProgramCode")
                 .OnDelete(DeleteBehavior.Cascade) 
       );



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
