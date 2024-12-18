using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class RunningSessionDetailsEF {
        public RunningSessionDetailsEF(int runningSessionId, int seqNr, int intervalTime, double intervalSpeed) {
            RunningSessionId = runningSessionId;
            SeqNr = seqNr;
            IntervalTime = intervalTime;
            IntervalSpeed = intervalSpeed;
        }

        [Key, Column("runningsession_id", Order = 1)] // Primaire sleutel - Deel 1
        public int RunningSessionId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column("seq_nr", Order = 2)] // Primaire sleutel - Deel 2
       
        public int SeqNr { get; set; }

        [Required]
        [Column("interval_time")]
        public int IntervalTime { get; set; }

        [Required]
        [Column("interval_speed")]
        public double IntervalSpeed { get; set; }
    }
}
