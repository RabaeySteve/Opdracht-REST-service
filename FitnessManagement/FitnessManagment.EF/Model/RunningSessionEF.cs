using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    public class RunningSessionEF {
        public RunningSessionEF() { }

        public RunningSessionEF(int runningSessionId, DateTime date, int duration, float avgSpeed, MemberEF member) {
            RunningSessionId = runningSessionId;
            Date = date;
            Duration = duration;
            AvgSpeed = avgSpeed;
            Member = member;
        }

        [Key]
        public int RunningSessionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        public float AvgSpeed { get; set; }

        // Foreign key
        
        public MemberEF Member { get; set; }


        public List<RunningSessionDetailsEF> Details { get; set; }
    }
}
