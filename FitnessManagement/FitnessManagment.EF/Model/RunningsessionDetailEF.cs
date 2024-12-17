using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class RunningSessionDetailsEF {
        public RunningSessionDetailsEF(int runningSessionId, int seqNr, int intervalTime, float intervalSpeed) {
            RunningSessionId = runningSessionId;
            SeqNr = seqNr;
            IntervalTime = intervalTime;
            IntervalSpeed = intervalSpeed;
        }

        [Column("runningsession_id")] // Primaire sleutel - Deel 1
        public int RunningSessionId { get; set; }

        [Column("seq_nr")] // Primaire sleutel - Deel 2
        [Required]
        public int SeqNr { get; set; }

        [Required]
        [Column("interval_time")]
        public int IntervalTime { get; set; }

        [Required]
        [Column("interval_speed")]
        public float IntervalSpeed { get; set; }
    }
}
