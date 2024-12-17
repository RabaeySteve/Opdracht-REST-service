using static FitnessManagement.BL.Models.CyclingSession;

namespace FitnessManagement.API.DTO_s {
    public class CyclingSessionDTO {
        public int CyclingSessionId { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int AvgWatt { get; set; }
        public int MaxCadence { get; set; }
        public int AvgCadence { get; set; }
        public int MaxWatt { get; set; }
        public string? Comment { get; set; }
        public CyclingTrainingType Type { get; set; }

    }
}
