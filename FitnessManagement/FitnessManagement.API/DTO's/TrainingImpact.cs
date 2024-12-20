namespace FitnessManagement.API.DTO_s {
    //public class TrainingImpact {

    //    public int Id { get; set; }
    //    public int MemberId { get; set; }
    //    public DateOnly Date { get; set; }
    //    public int Duration { get; set; }
    //    public string TrainingType { get; set; }

    //    public string? Impact { get; set; }

    //}

    public class TrainingTypeMonth {
        public DateOnly Date { get; set; }
        public string TrainingType { get; set; }
        
        public string? Impact { get; set; }
    }
}
