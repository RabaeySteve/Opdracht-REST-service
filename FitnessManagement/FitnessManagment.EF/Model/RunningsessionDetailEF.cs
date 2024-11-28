using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Model {
    public class RunningsessionDetailEF {

        [Key]
        public int SeqNr { get; set; } 

        [Required]
        public int IntervalTime { get; set; }

        public float IntervalSpeed { get; set; } 

        
        public int RunningSessionId { get; set; }
        public RunningSessionEF RunningSession { get; set; }

    }
}
