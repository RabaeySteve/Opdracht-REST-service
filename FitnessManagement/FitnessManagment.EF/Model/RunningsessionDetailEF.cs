using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Model {
    public class RusnningSessionDetailsEF {

        [Key]
        public int RusnningSessionId {  get; set; }
        [Required]
        public int SeqNr { get; set; } 

        [Required]
        public int IntervalTime { get; set; }
        [Required]
        public float IntervalSpeed { get; set; } 

        
        
        public RusnningSessionEF RusnningSession { get; set; }

    }
}
