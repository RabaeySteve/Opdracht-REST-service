using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Model {
    public class RunningSessionDetailsEF {
        public RunningSessionDetailsEF(int runningSessionId, int seqNr, int intervalTime, float intervalSpeed) {
            RunningSessionId = runningSessionId;
            SeqNr = seqNr;
            IntervalTime = intervalTime;
            IntervalSpeed = intervalSpeed;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RunningSessionId { get; set; }
        [Required]
        public int SeqNr { get; set; } 

        [Required]
        public int IntervalTime { get; set; }
        [Required]
        public float IntervalSpeed { get; set; } 

        
        
        

    }
}
