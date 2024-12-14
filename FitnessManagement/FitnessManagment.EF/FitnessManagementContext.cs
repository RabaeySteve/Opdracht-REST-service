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
        public DbSet<ProgramEF> programs { get; set; }
        public DbSet<RusnningSessionEF> RusnningSession_main { get; set; }
        public DbSet<CyclingSessionEF> CyclingSession { get; set; }
        public DbSet<EquipmentEF> equipment { get; set; }
        public DbSet<ReservationEF> reservation { get; set; }
        public DbSet<TimeSlotEF> time_slot { get; set; }
        public DbSet<RusnningSessionDetailsEF> RusnningSession_detail { get; set; }
        public DbSet<ProgramMember> programMember { get; set; }
        public FitnessManagementContext(string connectionString) {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ProgramMember>()
             .HasKey(mp => new { mp.MemberId, mp.ProgramCode }); // Composiete sleutel

            modelBuilder.Entity<ProgramMember>()
                .HasOne(mp => mp.Member)
                .WithMany(m => m.MemberPrograms)
                .HasForeignKey(mp => mp.MemberId);

            modelBuilder.Entity<ProgramMember>()
                .HasOne(mp => mp.Program)
                .WithMany()
                .HasForeignKey(mp => mp.ProgramCode); 

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
