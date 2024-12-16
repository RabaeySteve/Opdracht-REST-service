using static FitnessManagement.BL.Models.CyclingSession;

namespace FitnessManagement.API.DTO_s {
    public class CyclingSessionDTO {
        public int CyclingSessionId { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float AvgWatt { get; set; }
        public float MaxCadence { get; set; }
        public float AvgCadence { get; set; }
        public float MaxWatt { get; set; }
        public string Comment { get; set; }
        public CyclingTrainingType Type { get; set; }

    }
}
