namespace FitnessManagement.API.DTO_s {
    public class RunningSessionDTO {
        public int RunningSessionId { get; set; }
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float AvgSpeed { get; set; }
        public List<RunningSessionDetailDTO> Details { get; set; } = new List<RunningSessionDetailDTO>();

    }
    public class RunningSessionDetailDTO {
        public int SeqNr { get; set; }
        public int IntervalTime{ get; set; }
        public float IntervalSpeed { get; set; }
    }
}
