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

        public RunningSessionEF(int runningSessionId, DateTime date, int duration, float avgSpeed, int memberId) {
            RunningSessionId = runningSessionId;
            Date = date;
            Duration = duration;
            AvgSpeed = avgSpeed;
            MemberId = memberId;
        }

        [Key]
        public int RunningSessionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        public float AvgSpeed { get; set; }

        // Foreign key
        public int MemberId { get; set; }
        public MemberEF Member { get; set; }


        public List<RunningsessionDetailEF> Details { get; set; }
    }
}
