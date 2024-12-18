using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
   
    public class RunningSessionEF {
        public RunningSessionEF() { }

        public RunningSessionEF(int runningSessionId, DateTime date, int duration, double avgSpeed, MemberEF member, List<RunningSessionDetailsEF> details) {
            RunningSessionId = runningSessionId;
            Date = date;
            Duration = duration;
            AvgSpeed = avgSpeed;
            Member = member;
            Details = details;
        }

        [Key]
        [Column("runningsession_id")] 
        public int RunningSessionId { get; set; }

        [Required]
        [Column("date", TypeName = "datetime2(0)")]
        public DateTime Date { get; set; }

        [Required]
        [Column("duration")]
        public int Duration { get; set; }

        [Required]
        [Column("avg_speed")]
        public double AvgSpeed { get; set; }

        

        [ForeignKey("member_id")]
        public MemberEF Member { get; set; }

        [NotMapped] // Wordt niet in de tabel opgeslagen
        public List<RunningSessionDetailsEF> Details { get; set; }
    }
}
