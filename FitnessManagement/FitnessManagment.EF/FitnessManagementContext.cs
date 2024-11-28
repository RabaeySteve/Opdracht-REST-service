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

        

        public DbSet<MemberEF> Members { get; set; }
        public DbSet<ProgramEF> Programs { get; set; }
        public DbSet<RunningSessionEF> RunningSessions { get; set; }
        public DbSet<CyclingSessionEF> CyclingSessions { get; set; }
        public DbSet<EquipmentEF> Equipment { get; set; }
        public DbSet<ReservationEF> Reservations { get; set; }
        public DbSet<TimeSlotEF> TimeSlots { get; set; }


        protected FitnessManagementContext(string connectionString) {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
                

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
