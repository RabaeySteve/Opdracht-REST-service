using FitnessBL.Models;
using FitnessManagement.BL.Models;

namespace FitnessManagement.API.DTO_s {

    public class TrainingMapped {
        public int TrainingId { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public TrainingSessionType TrainingSessionType { get; set; }

    }
}
