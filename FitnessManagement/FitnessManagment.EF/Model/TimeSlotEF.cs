using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    public class TimeSlotEF {
        public TimeSlotEF() { }

        public TimeSlotEF(int timeSlotId, int startTime, int endTime, string partOfDay) {
            TimeSlotId = timeSlotId;
            StartTime = startTime;
            EndTime = endTime;
            PartOfDay = partOfDay;
        }

        [Key]
        public int TimeSlotId { get; set; }

        [Required]
        public int StartTime { get; set; }

        [Required]
        public int EndTime { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string PartOfDay { get; set; }
    }
}
