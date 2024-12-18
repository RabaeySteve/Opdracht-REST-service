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
        [Column("time_slot_id")] 
        public int TimeSlotId { get; set; }

        [Required]
        [Column("start_time")]
        public int StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public int EndTime { get; set; }

        [Required]
        [Column("part_of_day", TypeName = "nvarchar(20)")]
        public string PartOfDay { get; set; }
    }
}
